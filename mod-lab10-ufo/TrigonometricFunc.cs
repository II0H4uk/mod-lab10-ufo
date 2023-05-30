using System;

namespace mod_lab10_ufo {
    static class TrigonometricFunc {
        public static double Sin(double angle, int seriesLength) {
            double val = 0;
            for (int i = 1; i <= seriesLength; i++)
                val += Math.Pow(-1, i - 1) * Math.Pow(angle, 2 * i - 1) / Fact(2 * i - 1);
            return val;
        }

        public static double Cos(double angle, int seriesLength) {
            double val = 0;
            for (int i = 1; i <= seriesLength; i++)
                val += Math.Pow(-1, i - 1) * Math.Pow(angle, 2 * i - 2) / Fact(2 * i - 2);
            return val;
        }

        public static double Atan(double angle, int seriesLength) {
            double val = 0;
            for (int i = 1; i <= seriesLength; i++)
                val += Math.Pow(-1, i - 1) * Math.Pow(angle, 2 * i - 1) / (2 * i - 1);
            return val;
        }

        static double Fact(int num) {
            double val = 1;
            for (int i = 2; i <= num; i++)
                val *= i;
            return val;
        }
    }
}
