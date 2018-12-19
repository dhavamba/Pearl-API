namespace it.amalfi.Pearl
{
    /// <summary>
    /// A structure that represents a range of numbers
    /// </summary>
    public struct Range
    {
        #region Readonly Fields
        public readonly float min;
        public readonly float max;
        #endregion

        #region Constructors
        public Range(float min, float max)
        {
            if (min < max)
            {
                this.min = min;
                this.max = max;
            }
            else
            {
                this.min = max;
                this.max = min;
            }
        }
        #endregion
    }

}