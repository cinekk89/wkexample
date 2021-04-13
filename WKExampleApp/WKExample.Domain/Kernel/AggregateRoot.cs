namespace WKExample.Domain.Kernel
{
    public class AggregateRoot
    {
        protected int _version = 0;

        protected int Version => _version;

        //todo add domain events


        protected void IncrementVersion()
        {
            if (_version > 0)
                return;

            _version++;
        }
    }
}
