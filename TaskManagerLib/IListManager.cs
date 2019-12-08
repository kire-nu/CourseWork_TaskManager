using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    interface IListManager<T> {

        bool Add(T aType);

        bool ChangeAt(T aType, int aIndex);

        bool CheckIndex(int aIndex);

        bool DeleteAll();

        bool DeleteAt(int aIndex);

        T GetAt(int aIndex);

        string[] ToStringArray();

        List<string> ToStringList();

        void BinarySerialize(string fileName);
        void BinaryDeSerialize(string fileName);

    }
}
