namespace Zillow
{
    public class StringHelper {
        /// <summary>
        /// Compute a numeric representation of a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public long stringToLong(string s) {
            if (!this.validateString(s)) {
                return 0;
            }

            var isNegative = s[0] == '-' ? true : false;
            long number = 0;
            var index = isNegative ? 1 : 0;
            while (index < this.length(s)) {
                number *= 10;
                var digit = s[index++] - '0';
                number += digit;
            }

            return isNegative ? -1 * number : number;
        }

        private bool validateString(string s) {
            if (!string.IsNullOrEmpty(s)) {
                foreach (var c in s) {
                    if (c < '0' || c > '9') {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Private method to calculate length of a string s
        /// </summary>
        /// <param name="s">Input string whose length needs to be calculated</param>
        /// <returns>System.Int32 length of the string</returns>
        private int length(string s) {
            int length = 0;
            foreach(var c in s) {
                length++;
            }

            return length;
        }
    }
}
