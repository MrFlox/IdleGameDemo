using Modules.Core;
using Modules.Entities.Generator;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class Unload : ProcessNode
    {
        private readonly Field _field;
        private readonly IGameEntity<Collector.States> _collector;
        private CollectorModel _model;

        public Unload(Field field, IdleProcess<Collector> activeProcess, IGameEntity<Collector.States> collector,
            CollectorModel model) :
            base(activeProcess)
        {
            _field = field;
            _collector = collector;
            _model = model;
        }

        protected override void StartProcess()
        {
            _collector.SetState(Collector.States.Work);
            base.StartProcess();
        }

        protected override void OnFinishProcess()
        {
            _model.Copacity.Value -= 1;
            _field.Storage.Add();
        }
    }
}