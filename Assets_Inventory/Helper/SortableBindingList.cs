using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Assets_Inventory
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSortedValue;
        protected override PropertyDescriptor SortPropertyCore => sortPropertyValue;
        protected override ListSortDirection SortDirectionCore => sortDirectionValue;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var items = Items as List<T>;
            if (items != null)
            {
                items.Sort((x, y) =>
                {
                    var valueX = prop.GetValue(x);
                    var valueY = prop.GetValue(y);

                    if (valueX == null && valueY == null) return 0;
                    if (valueX == null) return -1;
                    if (valueY == null) return 1;

                    var comparable = valueX as IComparable;
                    if (comparable != null)
                    {
                        int result = comparable.CompareTo(valueY);
                        return direction == ListSortDirection.Ascending ? result : -result;
                    }
                    return 0;
                });

                isSortedValue = true;
                sortDirectionValue = direction;
                sortPropertyValue = prop;

                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override void RemoveSortCore()
        {
            isSortedValue = false;
        }
    }
}