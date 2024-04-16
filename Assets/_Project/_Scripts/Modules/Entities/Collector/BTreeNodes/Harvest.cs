using Modules.Core;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class Harvest : ProcessNode
    {
        private readonly Generator.Generator _activeGenerator;
        private readonly CollectorModel _model;
        private readonly Collector _collector;

        public Harvest(
            Generator.Generator activeGenerator,
            CollectorModel model,
            IdleProcess<Collector> activeProcess,
            Collector collector) : base(activeProcess)
        {
            _collector = collector;
            _activeGenerator = activeGenerator;
            _model = model;
        }

        protected override void StartProcess()
        {
            _collector.SetState(Collector.States.Work);
            base.StartProcess();
        }

        protected override void OnFinishProcess()
        {
            _activeGenerator.Collect();
            _collector.Harvest();
            _model.Copacity.Value += 1;
        }
    }
}