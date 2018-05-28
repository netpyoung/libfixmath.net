using fract32_t = System.UInt32;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;
using int32_t = System.Int32;

namespace libfixmath
{
    internal static partial class LibFixMath
    {
        public static fract32_t fract32_create(uint32_t inNumerator, uint32_t inDenominator)
        {
            if (inDenominator <= inNumerator)
                return 0xFFFFFFFF;
            uint32_t tempMod = (inNumerator % inDenominator);
            uint32_t tempDiv = (0xFFFFFFFF / (inDenominator - 1));
            return (tempMod * tempDiv);
        }

        public static fract32_t fract32_invert(fract32_t inFract)
        {
            return (0xFFFFFFFF - inFract);
        }

#if !FIXMATH_NO_64BIT
        public static uint32_t fract32_usmul(uint32_t inVal, fract32_t inFract)
        {
            return (uint32_t)(((uint64_t)inVal * (uint64_t)inFract) >> 32);
        }

        public static int32_t fract32_smul(int32_t inVal, fract32_t inFract)
        {
            if (inVal < 0)
                return -(int32_t)fract32_usmul((uint32_t)(-inVal), inFract);
            return (int32_t)fract32_usmul((uint32_t)inVal, inFract);
        }
    }
#endif
}
