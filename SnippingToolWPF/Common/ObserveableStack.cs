using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF.Common
{
    public class UndoRedo<T>
    {
        public Stack<T> undoStack = new Stack<T>();
        public Stack<T> redoStack = new Stack<T>();
        public ObservableCollection<T> liveCollection = new ObservableCollection<T>();

        public UndoRedoStack() 
        { 

        }


    }
}
