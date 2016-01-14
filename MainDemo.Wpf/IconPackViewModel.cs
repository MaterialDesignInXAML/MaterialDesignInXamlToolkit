using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    public class IconPackViewModel
    {
        private readonly Lazy<IEnumerable<PackIconKind>> _packIconKinds;

        public IconPackViewModel()
        {
            OpenDotComCommand = new AnotherCommandImplementation(OpenDotCom);
            _packIconKinds = new Lazy<IEnumerable<PackIconKind>>(() =>
                Enum.GetValues(typeof (PackIconKind)).OfType<PackIconKind>()
                    .OrderBy(k => k.ToString(), StringComparer.InvariantCultureIgnoreCase).ToList()
                );

        }

        public ICommand OpenDotComCommand { get; }

        public IEnumerable<PackIconKind> Kinds => _packIconKinds.Value;

        private void OpenDotCom(object obj)
        {
            Process.Start("https://materialdesignicons.com/");
        }


    }
}
