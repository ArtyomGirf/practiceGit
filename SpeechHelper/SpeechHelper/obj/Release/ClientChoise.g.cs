﻿#pragma checksum "..\..\ClientChoise.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5614F28C9A852DA6BC069400575594398EE482EED15F8EAA4B13859FDFCB0E71"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SpeechHelper;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SpeechHelper {
    
    
    /// <summary>
    /// ClientChoiseWindow
    /// </summary>
    public partial class ClientChoiseWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 72 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton name;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputClientName;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton inn;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputClientInn;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton id;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputClientId;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button searchClient;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addClient;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\ClientChoise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SpeechHelper;component/clientchoise.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ClientChoise.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.name = ((System.Windows.Controls.RadioButton)(target));
            
            #line 72 "..\..\ClientChoise.xaml"
            this.name.Checked += new System.Windows.RoutedEventHandler(this.NameChecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.inputClientName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.inn = ((System.Windows.Controls.RadioButton)(target));
            
            #line 78 "..\..\ClientChoise.xaml"
            this.inn.Checked += new System.Windows.RoutedEventHandler(this.InnChecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.inputClientInn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.id = ((System.Windows.Controls.RadioButton)(target));
            
            #line 84 "..\..\ClientChoise.xaml"
            this.id.Checked += new System.Windows.RoutedEventHandler(this.IdChecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.inputClientId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.searchClient = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.addClient = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.close = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\ClientChoise.xaml"
            this.close.Click += new System.Windows.RoutedEventHandler(this.СloseClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

