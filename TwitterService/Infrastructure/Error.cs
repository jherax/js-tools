namespace TwitterService.Infrastructure {

    /// <summary>
    /// Object used to transport error messages
    /// </summary>
    /// <!-- Created by: David Rivera -->
    internal sealed class Error {

        /// <summary>
        /// Constructores
        /// </summary>
        public Error() { }
        public Error(string message) {
            this.Message = message;
        }
        public string Message { get; set; }

    }//end class
}//end namespace