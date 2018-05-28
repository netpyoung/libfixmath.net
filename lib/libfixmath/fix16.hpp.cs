
using fix16_t = System.Int32;
using int16_t = System.Int16;

namespace libfixmath
{
    public struct fix16
    {
        private fix16_t value;

        public fix16(fix16 inValue)  { value = inValue.value;             }
        public fix16(fix16_t inValue) { value = inValue; }
        public fix16(float inValue) { value = LibFixMath.fix16_from_float(inValue); }
        public fix16(double inValue) { value = LibFixMath.fix16_from_dbl(inValue); }
        public fix16(int16_t inValue) { value = LibFixMath.fix16_from_int(inValue); }

        public static explicit operator fix16_t(fix16 x) { return x.value; }
        public static explicit operator double(fix16 x)   { return LibFixMath.fix16_to_dbl(x.value);   }
        public static explicit operator float(fix16 x)    { return LibFixMath.fix16_to_float(x.value); }
        public static explicit operator int16_t(fix16 x)  { return (int16_t)LibFixMath.fix16_to_int(x.value);   }
        public static explicit operator fix16(fix16_t rhs) { return new fix16(rhs); }
        public static explicit operator fix16(double rhs) { return new fix16(LibFixMath.fix16_from_dbl(rhs)); }
        public static explicit operator fix16(float rhs) { return new fix16(LibFixMath.fix16_from_float(rhs)); }
        public static explicit operator fix16(int16_t rhs) { return new fix16(LibFixMath.fix16_from_int(rhs)); }

        /*
        public static Fix16 operator+= (Fix16 self, Fix16 rhs) { self.value += rhs.value; return self; }
        public static Fix16 operator +=(Fix16 self, fix16_t rhs) { self.value += rhs; return self; }
        public static Fix16 operator +=(Fix16 self, double rhs) { self.value += LibFixMath.fix16_from_dbl(rhs); return self; }
        public static Fix16 operator +=(Fix16 self, float rhs) { self.value += LibFixMath.fix16_from_float(rhs); return self; }
        public static Fix16 operator +=(Fix16 self, int16_t rhs) { self.value += LibFixMath.fix16_from_int(rhs); return self; }
        public static Fix16 operator -=( Fix16 &rhs) { value -= rhs.value; return *this; }
public static Fix16 operator -=( fix16_t rhs) { value -= rhs; return *this; }
public static Fix16 operator -=( double rhs) { value -= fix16_from_dbl(rhs); return *this; }
public static Fix16 operator -=( float rhs) { value -= fix16_from_float(rhs); return *this; }
public static Fix16 operator -=( int16_t rhs) { value -= fix16_from_int(rhs); return *this; }

public static Fix16 operator *=( Fix16 &rhs) { value = fix16_mul(value, rhs.value); return *this; }
public static Fix16 operator *=( fix16_t rhs) { value = fix16_mul(value, rhs); return *this; }
public static Fix16 operator *=( double rhs) { value = fix16_mul(value, fix16_from_dbl(rhs)); return *this; }
public static Fix16 operator *=( float rhs) { value = fix16_mul(value, fix16_from_float(rhs)); return *this; }
public static Fix16 operator *=( int16_t rhs) { value *= rhs; return *this; }

public static Fix16 operator/=( Fix16 &rhs) { value = fix16_div(value, rhs.value); return *this; }
public static Fix16 operator/=( fix16_t rhs) { value = fix16_div(value, rhs); return *this; }
public static Fix16 operator/=( double rhs) { value = fix16_div(value, fix16_from_dbl(rhs)); return *this; }
public static Fix16 operator/=( float rhs) { value = fix16_div(value, fix16_from_float(rhs)); return *this; }
public static Fix16 operator/=( int16_t rhs) { value /= rhs; return *this; }
        */

        public static fix16 operator +(fix16 self, fix16 other)   { fix16 ret = self; ret += other; return ret; }
        public static fix16 operator +(fix16 self, fix16_t other)  { fix16 ret = self; ret += other; return ret; }
        public static fix16 operator +(fix16 self, double other)   { fix16 ret = self; ret += other; return ret; }
        public static fix16 operator +(fix16 self, float other)    { fix16 ret = self; ret += other; return ret; }
        public static fix16 operator +(fix16 self, int16_t other)  { fix16 ret = self; ret += other; return ret; }

#if !FIXMATH_NO_OVERFLOW
		fix16 sadd( fix16 other)   { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, other.value);             return ret; }
		fix16 sadd( fix16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, other);                   return ret; }
		fix16 sadd( double other)   { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, LibFixMath.fix16_from_dbl(other));   return ret; }
		fix16 sadd( float other)    { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, LibFixMath.fix16_from_float(other)); return ret; }
		fix16 sadd( int16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, LibFixMath.fix16_from_int(other));   return ret; }
#endif

        public static fix16 operator -(fix16 self, fix16 other)   { fix16 ret = self; ret -= other; return ret; }
        public static fix16 operator -(fix16 self, fix16_t other)  { fix16 ret = self; ret -= other; return ret; }
        public static fix16 operator -(fix16 self, double other)   { fix16 ret = self; ret -= other; return ret; }
        public static fix16 operator -(fix16 self, float other)    { fix16 ret = self; ret -= other; return ret; }
        public static fix16 operator -(fix16 self, int16_t other)  { fix16 ret = self; ret -= other; return ret; }

#if !FIXMATH_NO_OVERFLOW
		fix16 ssub( fix16 other)   { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, -other.value);             return ret; }
		fix16 ssub( fix16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, -other);                   return ret; }
		fix16 ssub( double other)   { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, -LibFixMath.fix16_from_dbl(other));   return ret; }
		fix16 ssub( float other)    { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, -LibFixMath.fix16_from_float(other)); return ret; }
		fix16 ssub( int16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_sadd(value, -LibFixMath.fix16_from_int(other));   return ret; }
#endif

        public static fix16 operator *(fix16 self, fix16 other)   { fix16 ret = self; ret *= other; return ret; }
        public static fix16 operator *(fix16 self, fix16_t other)  { fix16 ret = self; ret *= other; return ret; }
        public static fix16 operator *(fix16 self, double other)   { fix16 ret = self; ret *= other; return ret; }
        public static fix16 operator *(fix16 self, float other)    { fix16 ret = self; ret *= other; return ret; }
        public static fix16 operator *(fix16 self, int16_t other)  { fix16 ret = self; ret *= other; return ret; }

#if !FIXMATH_NO_OVERFLOW
	    fix16 smul( fix16 other)   { fix16 ret = (fix16)LibFixMath.fix16_smul(value, other.value);             return ret; }
		fix16 smul( fix16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_smul(value, other);                   return ret; }
		fix16 smul( double other)   { fix16 ret = (fix16)LibFixMath.fix16_smul(value, LibFixMath.fix16_from_dbl(other));   return ret; }
		fix16 smul( float other)    { fix16 ret = (fix16)LibFixMath.fix16_smul(value, LibFixMath.fix16_from_float(other)); return ret; }
		fix16 smul( int16_t other)  { fix16 ret = (fix16)LibFixMath.fix16_smul(value, LibFixMath.fix16_from_int(other));   return ret; }
#endif

        public static fix16 operator /(fix16 self, fix16 other)   { fix16 ret = self; ret /= other; return ret; }
        public static fix16 operator /(fix16 self, fix16_t other)  { fix16 ret = self; ret /= other; return ret; }
        public static fix16 operator /(fix16 self, double other)   { fix16 ret = self; ret /= other; return ret; }
        public static fix16 operator /(fix16 self, float other)    { fix16 ret = self; ret /= other; return ret; }
        public static fix16 operator /(fix16 self, int16_t other)  { fix16 ret = self; ret /= other; return ret; }

#if !FIXMATH_NO_OVERFLOW
		fix16 sdiv( fix16 other)  { fix16 ret = (fix16)LibFixMath.fix16_sdiv(value, other.value);             return ret; }
		fix16 sdiv( fix16_t other) { fix16 ret = (fix16)LibFixMath.fix16_sdiv(value, other);                   return ret; }
		fix16 sdiv( double other)  { fix16 ret = (fix16)LibFixMath.fix16_sdiv(value, LibFixMath.fix16_from_dbl(other));   return ret; }
		fix16 sdiv( float other)   { fix16 ret = (fix16)LibFixMath.fix16_sdiv(value, LibFixMath.fix16_from_float(other)); return ret; }
		fix16 sdiv( int16_t other) { fix16 ret = (fix16)LibFixMath.fix16_sdiv(value, LibFixMath.fix16_from_int(other));   return ret; }
#endif

        public static bool operator ==(fix16 self, fix16 other)   { return (self.value == other.value);             }
		public static bool operator ==(fix16 self, fix16_t other)  { return (self.value == other);                   }
		public static bool operator ==(fix16 self, double other)   { return (self.value == LibFixMath.fix16_from_dbl(other));   }
		public static bool operator ==(fix16 self, float other)    { return (self.value == LibFixMath.fix16_from_float(other)); }
		public static bool operator ==(fix16 self, int16_t other)  { return (self.value == LibFixMath.fix16_from_int(other));   }

		public static bool operator !=(fix16 self, fix16 other)   { return (self.value != other.value);             }
		public static bool operator !=(fix16 self, fix16_t other)  { return (self.value != other);                   }
		public static bool operator !=(fix16 self, double other)   { return (self.value != LibFixMath.fix16_from_dbl(other));   }
		public static bool operator !=(fix16 self, float other)    { return (self.value != LibFixMath.fix16_from_float(other)); }
		public static bool operator !=(fix16 self, int16_t other)  { return (self.value != LibFixMath.fix16_from_int(other));   }

		public static bool operator <=(fix16 self, fix16 other)   { return (self.value <= other.value);             }
		public static bool operator <=(fix16 self, fix16_t other)  { return (self.value <= other);                   }
		public static bool operator <=(fix16 self, double other)   { return (self.value <= LibFixMath.fix16_from_dbl(other));   }
		public static bool operator <=(fix16 self, float other)    { return (self.value <= LibFixMath.fix16_from_float(other)); }
		public static bool operator <=(fix16 self, int16_t other)  { return (self.value <= LibFixMath.fix16_from_int(other));   }

		public static bool operator >=(fix16 self, fix16 other)   { return (self.value >= other.value);             }
		public static bool operator >=(fix16 self, fix16_t other)  { return (self.value >= other);                   }
		public static bool operator >=(fix16 self, double other)   { return (self.value >= LibFixMath.fix16_from_dbl(other));   }
		public static bool operator >=(fix16 self, float other)    { return (self.value >= LibFixMath.fix16_from_float(other)); }
		public static bool operator >=(fix16 self, int16_t other)  { return (self.value >= LibFixMath.fix16_from_int(other));   }

		public static bool operator <(fix16 self, fix16 other)   { return (self.value <other.value);             }
		public static bool operator <(fix16 self, fix16_t other)  { return (self.value <other);                   }
		public static bool operator <(fix16 self, double other)   { return (self.value < LibFixMath.fix16_from_dbl(other));   }
		public static bool operator <(fix16 self, float other)    { return (self.value < LibFixMath.fix16_from_float(other)); }
		public static bool operator <(fix16 self, int16_t other)  { return (self.value < LibFixMath.fix16_from_int(other));   }

		public static bool operator >(fix16 self, fix16 other)   { return (self.value >  other.value);             }
		public static bool operator >(fix16 self, fix16_t other)  { return (self.value >  other);                   }
		public static bool operator >(fix16 self, double other)   { return (self.value > LibFixMath.fix16_from_dbl(other));   }
		public static bool operator >(fix16 self, float other)    { return (self.value > LibFixMath.fix16_from_float(other)); }
	    public static bool operator >(fix16 self, int16_t other)  { return (self.value > LibFixMath.fix16_from_int(other));   }

        public fix16 sin()  { return new fix16(LibFixMath.fix16_sin(value));  }
        public fix16 cos()  { return new fix16(LibFixMath.fix16_cos(value));  }
        public fix16 tan()  { return new fix16(LibFixMath.fix16_tan(value));  }
        public fix16 asin()  { return new fix16(LibFixMath.fix16_asin(value)); }
        public fix16 acos()  { return new fix16(LibFixMath.fix16_acos(value)); }
        public fix16 atan()  { return new fix16(LibFixMath.fix16_atan(value)); }
        public fix16 atan2(fix16 inY)  { return new fix16(LibFixMath.fix16_atan2(value, inY.value)); }
        public fix16 sqrt()  { return new fix16(LibFixMath.fix16_sqrt(value)); }

        public override bool Equals(object obj)
        {
            if (!(obj is fix16))
            {
                return false;
            }

            var fix = (fix16)obj;
            return this.value == fix.value;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }
}