using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Represents a step inside a <see cref="Stepper"/>.
    /// </summary>
    public interface IStep : INotifyPropertyChanged
    {
        /// <summary>
        /// The content of this step.
        /// </summary>
        object Content { get; set; }

        /// <summary>
        /// True, if this step is in an invalid semantic state.
        /// </summary>
        bool HasValidationErrors { get; set; }

        /// <summary>
        /// The header of this step.
        /// </summary>
        object Header { get; set; }

        /// <summary>
        /// Validates this steps.
        /// Inherited classes may implement this method.
        /// </summary>
        void Validate();
    }

    /// <summary>
    /// Basic implementation of <see cref="IStep"/>.
    /// Consider to inherit every of your steps from <see cref="Step"/> and define a data template for them.
    /// </summary>
    public class Step : IStep
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected object _header;
        protected object _content;
        protected bool _hasValidationErrors;

        /// <summary>
        /// The content of this step.
        /// </summary>
        public virtual object Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;

                OnPropertyChanged(nameof(Content));
            }
        }

        /// <summary>
        /// True, if this step is in an invalid semantic state.
        /// </summary>
        public virtual bool HasValidationErrors
        {
            get
            {
                return _hasValidationErrors;
            }

            set
            {
                _hasValidationErrors = value;

                OnPropertyChanged(nameof(HasValidationErrors));
            }
        }

        /// <summary>
        /// The header of this step.
        /// </summary>
        public virtual object Header
        {
            get
            {
                return _header;
            }

            set
            {
                _header = value;

                OnPropertyChanged(nameof(Header));
            }
        }

        public Step()
        {
            _header = null;
            _content = null;
            _hasValidationErrors = false;
        }

        /// <summary>
        /// Validates this steps.
        /// Inherited classes may implement this method.
        /// </summary>
        public virtual void Validate() { }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
