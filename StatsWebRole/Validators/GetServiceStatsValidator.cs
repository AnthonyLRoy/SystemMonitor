using StatsWebRole.Validators.Rules;

namespace StatsWebRole.Validators
{

    public class ServiceStatsValidator : ValidatorBase, IDataValidator
    {

        public ServiceStatsValidator()
        {
            BuildAssociations();
        }

        public void BuildAssociations()
        {
            ValidationRuleTable.Add("groupName", new RuleParameterAssociation("groupName", new StringIsNullorEmptyValidationRule()));

        }
    }
}
