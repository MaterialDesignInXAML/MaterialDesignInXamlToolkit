using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static partial class HintProxyFabric
    {
        private sealed class HintProxyBuilder
        {
            private readonly Func<Control, bool> _canBuild;
            private readonly Func<Control, IHintProxy> _build;

            public HintProxyBuilder(Func<Control, bool> canBuild, Func<Control, IHintProxy> build)
            {
                if (canBuild == null) throw new ArgumentNullException(nameof(canBuild));
                if (build == null) throw new ArgumentNullException(nameof(build));

                _canBuild = canBuild;
                _build = build;
            }

            public bool CanBuild(Control control) => _canBuild(control);
            public IHintProxy Build(Control control) => _build(control);
        }

        private static readonly List<HintProxyBuilder> Builders = new List<HintProxyBuilder>();

        static HintProxyFabric()
        {
            Builders.Add(new HintProxyBuilder(c => c is ComboBox, c => new ComboBoxHintProxy((ComboBox) c)));
            Builders.Add(new HintProxyBuilder(c => c is TextBox, c => new TextBoxHintProxy((TextBox)c)));
            Builders.Add(new HintProxyBuilder(c => c is PasswordBox, c => new PasswordBoxHintProxy((PasswordBox)c)));
        }

        public static void RegisterBuilder(Func<Control, bool> canBuild, Func<Control, IHintProxy> build)
        {
            Builders.Add(new HintProxyBuilder(canBuild, build));
        }

        public static IHintProxy Get(Control control)
        {
            var builder = Builders.FirstOrDefault(v => v.CanBuild(control));

            if (builder == null) throw new NotImplementedException();

            return builder.Build(control);
        }
    }
}
