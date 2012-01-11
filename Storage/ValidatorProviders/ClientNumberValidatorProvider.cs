using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Storage.ModelValidators;

namespace Storage.ValidatorProviders
{
    public class ClientNumberValidatorProvider : ClientDataTypeModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            bool isNumericField = base.GetValidators(metadata, context).Any();

            if (isNumericField)
            {
                yield return new ClientSideNumberValidator(metadata, context);
            }
        }
    } 
}