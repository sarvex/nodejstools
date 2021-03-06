﻿//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************//

using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

#if NTVS_FEATURE_INTERACTIVEWINDOW
namespace Microsoft.NodejsTools.Repl {
#else
namespace Microsoft.VisualStudio.Repl {
#endif
#if INTERACTIVE_WINDOW
    using IReplCommand = IInteractiveWindowCommand;
    using IReplWindow = IInteractiveWindow;
#endif

    [Export(typeof(IReplCommand))]
    class CancelExecutionCommand : IReplCommand {
        #region IReplCommand Members

        public Task<ExecutionResult> Execute(IReplWindow window, string arguments) {
            window.AbortCommand();
            return ExecutionResult.Succeeded;
        }

        public string Description {
            get { return "Stops execution of the current command."; }
        }

        public string Command {
            get { return null; }
        }

        public object ButtonContent {
            get {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.VisualStudio.Resources.CancelEvaluation.gif");
                image.EndInit();
                var res = new Image();
                res.Source = image;
                res.Width = res.Height = 16;
                return res;
            }
        }

        #endregion
    }
}
