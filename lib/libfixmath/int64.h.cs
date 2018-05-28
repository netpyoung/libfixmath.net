using System.Runtime.CompilerServices;
using int64_t = System.Int64;
using int32_t = System.Int32;
using uint32_t = System.UInt32;
using int8_t = System.Int16;

namespace libfixmath
{
    internal static partial class LibFixMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int64_t int64_const(int32_t hi, uint32_t lo) { return (((int64_t)hi << 32) | lo); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_from_int32(int32_t x) { return (int64_t)x; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int32_t int64_hi(int64_t x) { return (int32_t)(x >> 32); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint32_t int64_lo(int64_t x) { return (uint32_t)(x & ((1 << 32) - 1)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_add(int64_t x, int64_t y) { return (x + y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_neg(int64_t x) { return (-x); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_sub(int64_t x, int64_t y) { return (x - y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_shift(int64_t x, int8_t y) { return (y < 0 ? (x >> -y) : (x << y)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_mul_i32_i32(int32_t x, int32_t y) { return (x * y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_mul_i64_i32(int64_t x, int32_t y) { return (x * y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int64_t int64_div_i64_i32(int64_t x, int32_t y) { return (x / y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_eq(int64_t x, int64_t y) { return (x == y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_ne(int64_t x, int64_t y) { return (x != y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_gt(int64_t x, int64_t y) { return (x > y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_ge(int64_t x, int64_t y) { return (x >= y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_lt(int64_t x, int64_t y) { return (x < y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool int64_cmp_le(int64_t x, int64_t y) { return (x <= y); }
    }
}
