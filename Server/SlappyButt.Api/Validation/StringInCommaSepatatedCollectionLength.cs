namespace SlappyButt.Api.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using SlappyButt.Common.Constants;

    public class StringInCommaSepatatedCollectionLength : ValidationAttribute
    {
        private readonly int minimumLength;
        private readonly int maximumLength;
        private readonly string propertyName;

        public StringInCommaSepatatedCollectionLength(int minimumLength, int maximumLength, string propertyName)
        {
            this.propertyName = propertyName;
            this.minimumLength = minimumLength;
            this.maximumLength = maximumLength;
            this.ErrorMessage = ErrorMessages.PropertyNameLength;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (!string.IsNullOrWhiteSpace(valueAsString))
            {
                var tags = valueAsString
                    .Split(new[] { GlobalConstants.CommaSeparatedCollectionSeparator }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                return tags.All(tag => this.minimumLength <= tag.Trim().Length && tag.Trim().Length <= this.maximumLength);
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.propertyName, this.minimumLength, this.maximumLength);
        }
    }
}