using System;

namespace SourceGrid.Styles
{
	/// <summary>
	/// Summary description for Cell.
	/// </summary>
	public class StyleCell
	{
		/// <summary>
		/// 
		/// </summary>
		public StyleCell()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pIncludeType">Type of cells to apply this style, a cell can also derived by this type. If null all type are included.</param>
		/// <param name="pExcludeType">Type of cells to exclude, a cell can also derived by this type. If null all the cells are used.</param>
		/// <param name="pIncludeEditorValueType">Applied if the cell has the specified Type as a Editor.ValueType.</param>
		/// <param name="pView"></param>
		public StyleCell(Type pIncludeType, Type pExcludeType, Type pIncludeEditorValueType, Cells.Views.IView pView)
		{
			m_View = pView;
			m_IncludeType = pIncludeType;
			m_ExcludeType = pExcludeType;
			mIncludeEditorValueType = pIncludeEditorValueType;
		}

		private Cells.Views.IView m_View;
		public Cells.Views.IView View
		{
			get{return m_View;}
			set{m_View = value;}
		}

		private Type m_IncludeType;

		/// <summary>
		/// Type of cells to apply this style, a cell can also derived by this type. If null all type are included.
		/// </summary>
		public Type IncludeType
		{
			get{return m_IncludeType;}
			set{m_IncludeType = value;}
		}
		private Type m_ExcludeType;
		/// <summary>
		/// Type of cells to exclude, a cell can also derived by this type. If null all the cells are used.
		/// </summary>
		public Type ExcludeType
		{
			get{return m_ExcludeType;}
			set{m_ExcludeType = value;}
		}

		private Type mIncludeEditorValueType;

		/// <summary>
		/// Applied if the cell has the specified Type as a Editor.ValueType.
		/// </summary>
		public Type IncludeEditorValueType
		{
			get{return mIncludeEditorValueType;}
			set{mIncludeEditorValueType = value;}
		}

		public void ApplyToCell(Cells.ICellVirtual cell)
		{
			if (IncludeType == null || IncludeType.IsAssignableFrom(cell.GetType()))
			{
				if (ExcludeType == null || ExcludeType.IsAssignableFrom(cell.GetType()) == false)
				{
					if (IncludeEditorValueType == null || (cell.Editor != null && cell.Editor.ValueType == IncludeEditorValueType) )
						cell.View = View;
				}
			}
		}
	}


	/// <summary>
	/// A collection of elements of type StyleCell
	/// </summary>
	public class StyleCellCollection: System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the StyleCellCollection class.
		/// </summary>
		public StyleCellCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the StyleCellCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new StyleCellCollection.
		/// </param>
		public StyleCellCollection(StyleCell[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the StyleCellCollection class, containing elements
		/// copied from another instance of StyleCellCollection
		/// </summary>
		/// <param name="items">
		/// The StyleCellCollection whose elements are to be added to the new StyleCellCollection.
		/// </param>
		public StyleCellCollection(StyleCellCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this StyleCellCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this StyleCellCollection.
		/// </param>
		public virtual void AddRange(StyleCell[] items)
		{
			foreach (StyleCell item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another StyleCellCollection to the end of this StyleCellCollection.
		/// </summary>
		/// <param name="items">
		/// The StyleCellCollection whose elements are to be added to the end of this StyleCellCollection.
		/// </param>
		public virtual void AddRange(StyleCellCollection items)
		{
			foreach (StyleCell item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type StyleCell to the end of this StyleCellCollection.
		/// </summary>
		/// <param name="value">
		/// The StyleCell to be added to the end of this StyleCellCollection.
		/// </param>
		public virtual void Add(StyleCell value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic StyleCell value is in this StyleCellCollection.
		/// </summary>
		/// <param name="value">
		/// The StyleCell value to locate in this StyleCellCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this StyleCellCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(StyleCell value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this StyleCellCollection
		/// </summary>
		/// <param name="value">
		/// The StyleCell value to locate in the StyleCellCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(StyleCell value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the StyleCellCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the StyleCell is to be inserted.
		/// </param>
		/// <param name="value">
		/// The StyleCell to insert.
		/// </param>
		public virtual void Insert(int index, StyleCell value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the StyleCell at the given index in this StyleCellCollection.
		/// </summary>
		public virtual StyleCell this[int index]
		{
			get
			{
				return (StyleCell) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific StyleCell from this StyleCellCollection.
		/// </summary>
		/// <param name="value">
		/// The StyleCell value to remove from this StyleCellCollection.
		/// </param>
		public virtual void Remove(StyleCell value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by StyleCellCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			public Enumerator(StyleCellCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			public StyleCell Current
			{
				get
				{
					return (StyleCell) (this.wrapped.Current);
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (StyleCell) (this.wrapped.Current);
				}
			}

			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			public void Reset()
			{
				this.wrapped.Reset();
			}
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this StyleCellCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual StyleCellCollection.Enumerator GetEnumerator()
		{
			return new StyleCellCollection.Enumerator(this);
		}
	}

}
