
using uint32_t = System.UInt32;
using uint16_t = System.UInt16;
using int32_t = System.Int32;
using fix16_t = System.Int32;
using int64_t = System.Int64;
using uint8_t = System.UInt16;
using uint64_t = System.UInt64;

namespace libfixmath
{
    internal static partial class LibFixMath
    {
        /* Subtraction and addition with overflow detection.
         * The versions without overflow detection are inlined in the header.
         */
#if !FIXMATH_NO_OVERFLOW
        public static fix16_t fix16_add(fix16_t a, fix16_t b)
        {
            // Use unsigned integers because overflow with signed integers is
            // an undefined operation (http://www.airs.com/blog/archives/120).
            uint32_t _a = (uint32_t)a, _b = (uint32_t)b;
            uint32_t sum = _a + _b;

            // Overflow can only happen if sign of a == sign of b, and then
            // it causes sign of sum != sign of a.
            if (!(((_a ^ _b) & 0x80000000) != 0) && ((_a ^ sum) & 0x80000000) != 0)
                return fix16_overflow;

            return (fix16_t)sum;
        }

        public static fix16_t fix16_sub(fix16_t a, fix16_t b)
        {
            uint32_t _a = (uint32_t)a, _b = (uint32_t)b;
            uint32_t diff = _a - _b;

            // Overflow can only happen if sign of a != sign of b, and then
            // it causes sign of diff != sign of a.
            if ((((_a ^ _b) & 0x80000000 )!= 0) && (((_a ^ diff) & 0x80000000) != 0))
                return fix16_overflow;

            return (fix16_t)diff;
        }

        /* Saturating arithmetic */
        public static fix16_t fix16_sadd(fix16_t a, fix16_t b)
        {
            fix16_t result = fix16_add(a, b);

            if (result == fix16_overflow)
                return (a >= 0) ? fix16_maximum : fix16_minimum;

            return result;
        }

        public static fix16_t fix16_ssub(fix16_t a, fix16_t b)
        {
            fix16_t result = fix16_sub(a, b);

            if (result == fix16_overflow)
                return (a >= 0) ? fix16_maximum : fix16_minimum;

            return result;
        }
#endif



        /* 64-bit implementation for fix16_mul. Fastest version for e.g. ARM Cortex M3.
         * Performs a 32*32 -> 64bit multiplication. The middle 32 bits are the result,
         * bottom 16 bits are used for rounding, and upper 16 bits are used for overflow
         * detection.
         */

        public static fix16_t fix16_mul(fix16_t inArg0, fix16_t inArg1)
        {
            int64_t product = (int64_t)inArg0 * inArg1;

#if !FIXMATH_NO_OVERFLOW
            // The upper 17 bits should all be the same (the sign).
            uint32_t upper = (uint32_t)(product >> 47);
#endif

            if (product < 0)
            {
#if !FIXMATH_NO_OVERFLOW
                if (upper == 0)
                    return fix16_overflow;
#endif

#if !FIXMATH_NO_ROUNDING
                // This adjustment is required in order to round -1/2 correctly
                product--;
#endif
            }
            else
            {
#if !FIXMATH_NO_OVERFLOW
                if (upper != 0)
                    return fix16_overflow;
#endif
            }

#if FIXMATH_NO_ROUNDING
            return product >> 16;
#else
            fix16_t result = (fix16_t)(product >> 16);
            result += (fix16_t)(product & 0x8000) >> 15;

            return result;
#endif
        }

#if !FIXMATH_NO_OVERFLOW
/* Wrapper around fix16_mul to add saturating arithmetic. */
public static fix16_t fix16_smul(fix16_t inArg0, fix16_t inArg1)
{
    fix16_t result = fix16_mul(inArg0, inArg1);

    if (result == fix16_overflow)
    {
        if ((inArg0 >= 0) == (inArg1 >= 0))
            return fix16_maximum;
        else
            return fix16_minimum;
    }

    return result;
}
#endif

        public static uint8_t clz(uint32_t x)
        {
	        uint8_t result = 0;
	        if (x == 0) return 32;
	        while (!((x & 0xF0000000) != 0)) { result += 4; x <<= 4; }
	        while (!((x & 0x80000000) != 0)) { result += 1; x <<= 1; }
	        return result;
        }

        public static fix16_t fix16_div(fix16_t a, fix16_t b)
        {
            // This uses a hardware 32/32 bit division multiple times, until we have
            // computed all the bits in (a<<17)/b. Usually this takes 1-3 iterations.

            if (b == 0)
                return fix16_minimum;

            uint32_t remainder = (uint32_t)((a >= 0) ? a : (-a));
            uint32_t divider = (uint32_t)((b >= 0) ? b : (-b));
            uint32_t quotient = 0;
            int bit_pos = 17;

            // Kick-start the division a bit.
            // This improves speed in the worst-case scenarios where N and D are large
            // It gets a lower estimate for the result by N/(D >> 17 + 1).
            if ((divider & 0xFFF00000) != 0)
            {
                uint32_t shifted_div = ((divider >> 17) + 1);
                quotient = remainder / shifted_div;
                remainder -= (uint32_t)((uint64_t)quotient * (uint64_t)divider) >> 17;
            }

            // If the divider is divisible by 2^n, take advantage of it.
            while (!((divider & 0xF) != 0) && bit_pos >= 4)
            {
                divider >>= 4;
                bit_pos -= 4;
            }


            while ((remainder & bit_pos) >= 0)
            {
                // Shift remainder as much as we can without overflowing
                int shift = clz(remainder);
                if (shift > bit_pos)
                    shift = bit_pos;
                remainder <<= shift;
                bit_pos -= shift;

                uint32_t div = remainder / divider;
                remainder = remainder % divider;
                quotient += div << bit_pos;

        #if !FIXMATH_NO_OVERFLOW
                if ((div & ~(0xFFFFFFFF >> bit_pos)) != 0)
                    return fix16_overflow;
        #endif

                remainder <<= 1;
                bit_pos--;
            }

        #if !FIXMATH_NO_ROUNDING
            // Quotient is always positive so rounding is easy
            quotient++;
#endif

            fix16_t result = (fix16_t)(quotient >> 1);

            // Figure out the sign of the result
            if (((a ^ b) & 0x80000000) != 0L)
            {
        #if !FIXMATH_NO_OVERFLOW
                if (result == fix16_minimum)
                    return fix16_overflow;
        #endif

                result = -result;
            }

            return result;
        }


#if !FIXMATH_NO_OVERFLOW
        /* Wrapper around fix16_div to add saturating arithmetic. */
        public static fix16_t fix16_sdiv(fix16_t inArg0, fix16_t inArg1)
{
    fix16_t result = fix16_div(inArg0, inArg1);

    if (result == fix16_overflow)
    {
        if ((inArg0 >= 0) == (inArg1 >= 0))
            return fix16_maximum;
        else
            return fix16_minimum;
    }

    return result;
}
#endif

public static fix16_t fix16_mod(fix16_t x, fix16_t y)
{
    /* Note that in C90, the sign of result of the modulo operation is
     * undefined. in C99, it's the same as the dividend (aka numerator).
     */
    x %= y;
    return x;
}


        public static fix16_t fix16_lerp8(fix16_t inArg0, fix16_t inArg1, uint8_t inFract)
        {
            int64_t tempOut = int64_mul_i32_i32(inArg0, ((1 << 8) - inFract));
            tempOut = int64_add(tempOut, int64_mul_i32_i32(inArg1, inFract));
            tempOut = int64_shift(tempOut, -8);
            return (fix16_t)int64_lo(tempOut);
        }

        public static fix16_t fix16_lerp16(fix16_t inArg0, fix16_t inArg1, uint16_t inFract)
        {
            int64_t tempOut = int64_mul_i32_i32(inArg0, (((int32_t)1 << 16) - inFract));
            tempOut = int64_add(tempOut, int64_mul_i32_i32(inArg1, inFract));
            tempOut = int64_shift(tempOut, -16);
            return (fix16_t)int64_lo(tempOut);
        }

        public static fix16_t fix16_lerp32(fix16_t inArg0, fix16_t inArg1, uint32_t inFract)
        {
            int64_t tempOut;
            tempOut = ((int64_t)inArg0 * (0 - inFract));
            tempOut += ((int64_t)inArg1 * inFract);
            tempOut >>= 32;
            return (fix16_t)tempOut;
        }
    }
}
