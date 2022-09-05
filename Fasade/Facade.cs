using System.Text;

namespace Fasade
{
    internal class Facade
    {
        protected Subsystem1 _subsystem1;

        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            this._subsystem1 = subsystem1;
            this._subsystem2 = subsystem2;
        }

        public string Operation()
        {
            var builder = new StringBuilder($"Facade initializes subsystems:{Environment.NewLine}");
            builder.AppendLine(this._subsystem1.operation1());
            builder.AppendLine(this._subsystem2.operation1());
            builder.AppendLine($"Facade orders subsystems to perform the action:{Environment.NewLine}");
            builder.AppendLine(this._subsystem1.operationN());
            builder.AppendLine(this._subsystem2.operationZ());
            return builder.ToString();
        }
    }
}
