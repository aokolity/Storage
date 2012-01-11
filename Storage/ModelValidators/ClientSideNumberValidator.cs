using System.Collections.Generic;
using System.Web.Mvc;

namespace Storage.ModelValidators
{
    public class ClientSideNumberValidator : ModelValidator
    {
        private const string MustBeNumberValidationMessage = "Значение {0} должно быть числом.";

        public ClientSideNumberValidator(ModelMetadata metadata, ControllerContext controllerContext): base(metadata, controllerContext) { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            yield break; // Do nothing for server-side validation 
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule
            {
                ValidationType = "number",
                ErrorMessage = string.Format(MustBeNumberValidationMessage, Metadata.GetDisplayName())
            };
        }
    } 
}