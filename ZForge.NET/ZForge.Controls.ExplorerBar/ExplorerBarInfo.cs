/*
 * Copyright ?2004-2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace ZForge.Controls.ExplorerBar
{
	#region ExplorerBarInfo Class
	
	/// <summary>
	/// A class that contains system defined settings for an XPExplorerBar
	/// </summary>
	public class ExplorerBarInfo : IDisposable
	{
		#region Class Data
		
		/// <summary>
		/// System defined settings for a TaskPane
		/// </summary>
		private TaskPaneInfo taskPane;

		/// <summary>
		/// System defined settings for a TaskItem
		/// </summary>
		private TaskItemInfo taskItem;

		/// <summary>
		/// System defined settings for an Expando
		/// </summary>
		private ExpandoInfo expando;

		/// <summary>
		/// System defined settings for an Expando's header
		/// </summary>
		private HeaderInfo header;

		/// <summary>
		/// Specifies whether the ExplorerBarInfo represents an 
		/// official Windows XP theme
		/// </summary>
		private bool officialTheme;

		/// <summary>
		/// Specifies whether the ExplorerBarInfo represents the 
		/// Windows XP "classic" theme
		/// </summary>
		private bool classicTheme;

		/// <summary>
		/// A string that contains the full path to the ShellStyle.dll 
		/// that the ExplorerBarInfo was loaded from
		/// </summary>
		private string shellStylePath;

		#endregion

		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ExplorerBarInfo class with 
		/// default settings
		/// </summary>
		public ExplorerBarInfo()
		{
			this.taskPane = new TaskPaneInfo();
			this.taskItem = new TaskItemInfo();
			this.expando = new ExpandoInfo();
			this.header = new HeaderInfo();

			this.officialTheme = false;
			this.classicTheme = false;
			this.shellStylePath = null;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Sets the arrow images for use when theming is not supported
		/// </summary>
		public void SetUnthemedArrowImages()
		{
			this.Header.SetUnthemedArrowImages();
		}


		/// <summary>
		/// Force use of default values
		/// </summary>
		public void UseClassicTheme()
		{
			this.classicTheme = true;
			
			this.TaskPane.SetDefaultValues();
			this.Expando.SetDefaultValues();
			this.Header.SetDefaultValues();
			this.TaskItem.SetDefaultValues();

			this.SetUnthemedArrowImages();
		}


		/// <summary>
		/// Releases all resources used by the ExplorerBarInfo
		/// </summary>
		public void Dispose()
		{
			this.taskPane.Dispose();
			this.header.Dispose();
			this.expando.Dispose();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the ExplorerPane settings
		/// </summary>
		public TaskPaneInfo TaskPane
		{
			get
			{
				return this.taskPane;
			}

			set
			{
				this.taskPane = value;
			}
		}


		/// <summary>
		/// Gets the TaskLink settings
		/// </summary>
		public TaskItemInfo TaskItem
		{
			get
			{
				return this.taskItem;
			}

			set
			{
				this.taskItem = value;
			}
		}


		/// <summary>
		/// Gets the Group settings
		/// </summary>
		public ExpandoInfo Expando
		{
			get
			{
				return this.expando;
			}

			set
			{
				this.expando = value;
			}
		}


		/// <summary>
		/// Gets the Header settings
		/// </summary>
		public HeaderInfo Header
		{
			get
			{
				return this.header;
			}

			set
			{
				this.header = value;
			}
		}


		/// <summary>
		/// Gets whether the ExplorerBarInfo contains settings for 
		/// an official Windows XP Visual Style
		/// </summary>
		public bool OfficialTheme
		{
			get
			{
				return this.officialTheme;
			}

			/*set
			{
				this.officialTheme = value;
			}*/
		}


		/// <summary>
		/// Sets whether the ExplorerBarInfo contains settings for 
		/// an official Windows XP Visual Style
		/// </summary>
		/// <param name="officialTheme">true if the ExplorerBarInfo 
		/// contains settings for an official Windows XP Visual Style, 
		/// otherwise false</param>
		internal void SetOfficialTheme(bool officialTheme)
		{
			this.officialTheme = officialTheme;
		}


		/// <summary>
		/// Gets whether the ExplorerBarInfo contains settings for 
		/// the Windows XP "classic" Visual Style
		/// </summary>
		public bool ClassicTheme
		{
			get
			{
				return this.classicTheme;
			}
		}


		/// <summary>
		/// Gets or sets a string that specifies the full path to the 
		/// ShellStyle.dll that the ExplorerBarInfo was loaded from
		/// </summary>
		public string ShellStylePath
		{
			get
			{
				return this.shellStylePath;
			}

			set
			{
				this.shellStylePath = value;
			}
		}

		#endregion


		#region ExplorerBarInfoSurrogate

		/// <summary>
		/// A class that is serialized instead of an ExplorerBarInfo (as 
		/// ExplorerBarInfos contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class ExplorerBarInfoSurrogate : ISerializable
		{
			#region Class Data

			/// <summary>
			/// This member is not intended to be used directly from your code.
			/// </summary>
			public TaskPaneInfo.TaskPaneInfoSurrogate TaskPaneInfoSurrogate;
			
			/// <summary>
			/// This member is not intended to be used directly from your code.
			/// </summary>
			public TaskItemInfo.TaskItemInfoSurrogate TaskItemInfoSurrogate;
			
			/// <summary>
			/// This member is not intended to be used directly from your code.
			/// </summary>
			public ExpandoInfo.ExpandoInfoSurrogate ExpandoInfoSurrogate;
			
			/// <summary>
			/// This member is not intended to be used directly from your code.
			/// </summary>
			public HeaderInfo.HeaderInfoSurrogate HeaderInfoSurrogate;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the ExplorerBarInfoSurrogate class with default settings
			/// </summary>
			public ExplorerBarInfoSurrogate()
			{
				this.TaskPaneInfoSurrogate = null;
				this.TaskItemInfoSurrogate = null;
				this.ExpandoInfoSurrogate = null;
				this.HeaderInfoSurrogate = null;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the ExplorerBarInfoSurrogate with data that is to be 
			/// serialized from the specified ExplorerBarInfo
			/// </summary>
			/// <param name="explorerBarInfo">The ExplorerBarInfo that contains the data 
			/// to be serialized</param>
			public void Load(ExplorerBarInfo explorerBarInfo)
			{
				this.TaskPaneInfoSurrogate = new TaskPaneInfo.TaskPaneInfoSurrogate();
				this.TaskPaneInfoSurrogate.Load(explorerBarInfo.TaskPane);

				this.TaskItemInfoSurrogate = new TaskItemInfo.TaskItemInfoSurrogate();
				this.TaskItemInfoSurrogate.Load(explorerBarInfo.TaskItem);

				this.ExpandoInfoSurrogate = new ExpandoInfo.ExpandoInfoSurrogate();
				this.ExpandoInfoSurrogate.Load(explorerBarInfo.Expando);

				this.HeaderInfoSurrogate = new HeaderInfo.HeaderInfoSurrogate();
				this.HeaderInfoSurrogate.Load(explorerBarInfo.Header);
			}


			/// <summary>
			/// Returns an ExplorerBarInfo that contains the deserialized ExplorerBarInfoSurrogate data
			/// </summary>
			/// <returns>An ExplorerBarInfo that contains the deserialized ExplorerBarInfoSurrogate data</returns>
			public ExplorerBarInfo Save()
			{
				ExplorerBarInfo explorerBarInfo = new ExplorerBarInfo();

				explorerBarInfo.TaskPane = this.TaskPaneInfoSurrogate.Save();
				explorerBarInfo.TaskItem = this.TaskItemInfoSurrogate.Save();
				explorerBarInfo.Expando = this.ExpandoInfoSurrogate.Save();
				explorerBarInfo.Header = this.HeaderInfoSurrogate.Save();				
				
				return explorerBarInfo;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the ExplorerBarInfoSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("TaskPaneInfoSurrogate", this.TaskPaneInfoSurrogate);
				info.AddValue("TaskItemInfoSurrogate", this.TaskItemInfoSurrogate);
				info.AddValue("ExpandoInfoSurrogate", this.ExpandoInfoSurrogate);
				info.AddValue("HeaderInfoSurrogate", this.HeaderInfoSurrogate);
			}


			/// <summary>
			/// Initializes a new instance of the ExplorerBarInfoSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the ExplorerBarInfoSurrogate</param>
			/// <param name="context">The source from which the ExplorerBarInfoSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected ExplorerBarInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.TaskPaneInfoSurrogate = (TaskPaneInfo.TaskPaneInfoSurrogate) info.GetValue("TaskPaneInfoSurrogate", typeof(TaskPaneInfo.TaskPaneInfoSurrogate));
				this.TaskItemInfoSurrogate = (TaskItemInfo.TaskItemInfoSurrogate) info.GetValue("TaskItemInfoSurrogate", typeof(TaskItemInfo.TaskItemInfoSurrogate));
				this.ExpandoInfoSurrogate = (ExpandoInfo.ExpandoInfoSurrogate) info.GetValue("ExpandoInfoSurrogate", typeof(ExpandoInfo.ExpandoInfoSurrogate));
				this.HeaderInfoSurrogate = (HeaderInfo.HeaderInfoSurrogate) info.GetValue("HeaderInfoSurrogate", typeof(HeaderInfo.HeaderInfoSurrogate));
			}

			#endregion
		}

		#endregion
	}

	#endregion


	#region TaskPaneInfo Class

	/// <summary>
	/// A class that contains system defined settings for TaskPanes
	/// </summary>
	public class TaskPaneInfo : IDisposable
	{
		#region Class Data
		
		/// <summary>
		/// The starting Color for the TaskPane's background gradient
		/// </summary>
		private Color gradientStartColor;
		
		/// <summary>
		/// The ending Color for the TaskPane's background gradient
		/// </summary>
		private Color gradientEndColor;

		/// <summary>
		/// The direction of the TaskPane's gradient background
		/// </summary>
		private LinearGradientMode direction;

		/// <summary>
		/// The amount of space between the Border and Expandos along 
		/// each edge of the TaskPane
		/// </summary>
		private Padding padding;

		/// <summary>
		/// The Image that is used as the TaskPane's background
		/// </summary>
		private Image backImage;

		/// <summary>
		/// Specified how the TaskPane's background Image is drawn
		/// </summary>
		private ImageStretchMode stretchMode;

		/// <summary>
		/// The Image that is used as a watermark
		/// </summary>
		private Image watermark;

		/// <summary>
		/// The alignment of the Image used as a watermark
		/// </summary>
		private ContentAlignment watermarkAlignment;

		/// <summary>
		/// The TaskPane that owns the TaskPaneInfo
		/// </summary>
		private TaskPane owner;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TaskPaneInfo class with default settings
		/// </summary>
		public TaskPaneInfo()
		{
			// set background values
			this.gradientStartColor = Color.Transparent;
			this.gradientEndColor = Color.Transparent;
			this.direction = LinearGradientMode.Vertical;

			// set padding values
			this.padding = new Padding(12, 12, 12, 12);

			// images
			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;

			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;

			this.owner = null;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Forces the use of default values
		/// </summary>
		public void SetDefaultValues()
		{
			// set background values
			this.gradientStartColor = SystemColors.Window;
			this.gradientEndColor = SystemColors.Window;
			this.direction = LinearGradientMode.Vertical;

			// set padding values
			this.padding.Left = 12;
			this.padding.Top = 12;
			this.padding.Right = 12;
			this.padding.Bottom = 12;

			// images
			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;
			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;
		}


		/// <summary>
		/// Forces the use of default empty values
		/// </summary>
		public void SetDefaultEmptyValues()
		{
			// set background values
			this.gradientStartColor = Color.Empty;
			this.gradientEndColor = Color.Empty;
			this.direction = LinearGradientMode.Vertical;

			// set padding values
			this.padding.Left = 0;
			this.padding.Top = 0;
			this.padding.Right = 0;
			this.padding.Bottom = 0;

			// images
			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;
			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;
		}


		/// <summary>
		/// Releases all resources used by the TaskPaneInfo
		/// </summary>
		public void Dispose()
		{
			if (this.backImage != null)
			{
				this.backImage.Dispose();
				this.backImage = null;
			}

			if (this.watermark != null)
			{
				this.watermark.Dispose();
				this.watermark = null;
			}
		}

		#endregion


		#region Properties

		#region Background

		/// <summary>
		/// Gets or sets the TaskPane's first gradient background color
		/// </summary>
		[Description("The TaskPane's first gradient background color")]
		public Color GradientStartColor
		{
			get
			{
				return this.gradientStartColor;
			}

			set
			{
				if (this.gradientStartColor != value)
				{
					this.gradientStartColor = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the GradientStartColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the GradientStartColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeGradientStartColor()
		{
			return this.GradientStartColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the TaskPane's second gradient background color
		/// </summary>
		[Description("The TaskPane's second gradient background color")]
		public Color GradientEndColor
		{
			get
			{
				return this.gradientEndColor;
			}

			set
			{
				if (this.gradientEndColor != value)
				{
					this.gradientEndColor = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the GradientEndColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the GradientEndColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeGradientEndColor()
		{
			return this.GradientEndColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the direction of the TaskPane's gradient
		/// </summary>
		[DefaultValue(LinearGradientMode.Vertical),
		Description("The direction of the TaskPane's background gradient")]
		public LinearGradientMode GradientDirection
		{
			get
			{
				return this.direction;
			}

			set
			{
				if (!Enum.IsDefined(typeof(LinearGradientMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(LinearGradientMode));
				}

				if (this.direction != value)
				{
					this.direction = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region Images

		/// <summary>
		/// Gets or sets the Image that is used as the TaskPane's background
		/// </summary>
		[DefaultValue(null),
		Description("The Image that is used as the TaskPane's background")]
		public Image BackImage
		{
			get
			{
				return this.backImage;
			}

			set
			{
				if (this.backImage != value)
				{
					this.backImage = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets how the TaskPane's background Image is drawn
		/// </summary>
		[Browsable(false),
		DefaultValue(ImageStretchMode.Tile),
		Description("Specifies how the TaskPane's background Image is drawn")]
		public ImageStretchMode StretchMode
		{
			get
			{
				return this.stretchMode;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ImageStretchMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ImageStretchMode));
				}

				if (this.stretchMode != value)
				{
					this.stretchMode = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the Image that is used as the TaskPane's watermark
		/// </summary>
		[DefaultValue(null),
		Description("The Image that is used as the TaskPane's watermark")]
		public Image Watermark
		{
			get
			{
				return this.watermark;
			}

			set
			{
				if (this.watermark != value)
				{
					this.watermark = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the Image that is used as the 
		/// TaskPane's watermark
		/// </summary>
		[DefaultValue(ContentAlignment.BottomCenter),
		Description("The alignment of the Image that is used as the TaskPane's watermark")]
		public ContentAlignment WatermarkAlignment
		{
			get
			{
				return this.watermarkAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.watermarkAlignment != value)
				{
					this.watermarkAlignment = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets or sets the TaskPane's padding between the border and any items
		/// </summary>
		[Description("The amount of space between the border and the Expando's along each side of the TaskPane")]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				if (this.padding != value)
				{
					this.padding = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Padding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the Padding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}

		#endregion

		#region TaskPane

		/// <summary>
		/// Gets or sets the TaskPane the TaskPaneInfo belongs to
		/// </summary>
		protected internal TaskPane TaskPane
		{
			get
			{
				return this.owner;
			}
			
			set
			{
				this.owner = value;
			}
		}

		#endregion

		#endregion


		#region TaskPaneInfoSurrogate

		/// <summary>
		/// A class that is serialized instead of a TaskPaneInfo (as 
		/// TaskPaneInfos contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class TaskPaneInfoSurrogate : ISerializable
		{
			#region Class Data
			
			/// <summary>
			/// See TaskPaneInfo.GradientStartColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string GradientStartColor;
			
			/// <summary>
			/// See TaskPaneInfo.GradientEndColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string GradientEndColor;
			
			/// <summary>
			/// See TaskPaneInfo.GradientDirection.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public LinearGradientMode GradientDirection;
			
			/// <summary>
			/// See TaskPaneInfo.Padding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding Padding;
			
			/// <summary>
			/// See TaskPaneInfo.BackImage.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("BackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] BackImage;
			
			/// <summary>
			/// See TaskPaneInfo.StretchMode.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public ImageStretchMode StretchMode;
			
			/// <summary>
			/// See TaskPaneInfo.Watermark.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("Watermark", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Watermark;
			
			/// <summary>
			/// See TaskPaneInfo.WatermarkAlignment.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public ContentAlignment WatermarkAlignment;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the TaskPaneInfoSurrogate class with default settings
			/// </summary>
			public TaskPaneInfoSurrogate()
			{
				this.GradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientDirection = LinearGradientMode.Vertical;

				this.Padding = Padding.Empty;

				this.BackImage = new byte[0];
				this.StretchMode = ImageStretchMode.Normal;

				this.Watermark = new byte[0];
				this.WatermarkAlignment = ContentAlignment.BottomCenter;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the TaskPaneInfoSurrogate with data that is to be 
			/// serialized from the specified TaskPaneInfo
			/// </summary>
			/// <param name="taskPaneInfo">The TaskPaneInfo that contains the data 
			/// to be serialized</param>
			public void Load(TaskPaneInfo taskPaneInfo)
			{
				this.GradientStartColor = ThemeManager.ConvertColorToString(taskPaneInfo.GradientStartColor);
				this.GradientEndColor = ThemeManager.ConvertColorToString(taskPaneInfo.GradientEndColor);
				this.GradientDirection = taskPaneInfo.GradientDirection;

				this.Padding = taskPaneInfo.Padding;

				this.BackImage = ThemeManager.ConvertImageToByteArray(taskPaneInfo.BackImage);
				this.StretchMode = taskPaneInfo.StretchMode;

				this.Watermark = ThemeManager.ConvertImageToByteArray(taskPaneInfo.Watermark);
				this.WatermarkAlignment = taskPaneInfo.WatermarkAlignment;
			}


			/// <summary>
			/// Returns a TaskPaneInfo that contains the deserialized TaskPaneInfoSurrogate data
			/// </summary>
			/// <returns>A TaskPaneInfo that contains the deserialized TaskPaneInfoSurrogate data</returns>
			public TaskPaneInfo Save()
			{
				TaskPaneInfo taskPaneInfo = new TaskPaneInfo();

				taskPaneInfo.GradientStartColor = ThemeManager.ConvertStringToColor(this.GradientStartColor);
				taskPaneInfo.GradientEndColor = ThemeManager.ConvertStringToColor(this.GradientEndColor);
				taskPaneInfo.GradientDirection = this.GradientDirection;

				taskPaneInfo.Padding = this.Padding;

				taskPaneInfo.BackImage = ThemeManager.ConvertByteArrayToImage(this.BackImage);
				taskPaneInfo.StretchMode = this.StretchMode;

				taskPaneInfo.Watermark = ThemeManager.ConvertByteArrayToImage(this.Watermark);
				taskPaneInfo.WatermarkAlignment = this.WatermarkAlignment;
				
				return taskPaneInfo;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the TaskPaneInfoSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("GradientStartColor", this.GradientStartColor);
				info.AddValue("GradientEndColor", this.GradientEndColor);
				info.AddValue("GradientDirection", this.GradientDirection);
				
				info.AddValue("Padding", this.Padding);
				
				info.AddValue("BackImage", this.BackImage);
				info.AddValue("StretchMode", this.StretchMode);
				
				info.AddValue("Watermark", this.Watermark);
				info.AddValue("WatermarkAlignment", this.WatermarkAlignment);
			}


			/// <summary>
			/// Initializes a new instance of the TaskPaneInfoSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the TaskPaneInfoSurrogate</param>
			/// <param name="context">The source from which the TaskPaneInfoSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskPaneInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.GradientStartColor = info.GetString("GradientStartColor");
				this.GradientEndColor = info.GetString("GradientEndColor");
				this.GradientDirection = (LinearGradientMode) info.GetValue("GradientDirection", typeof(LinearGradientMode));
				
				this.Padding = (Padding) info.GetValue("Padding", typeof(Padding));

				this.BackImage = (byte[]) info.GetValue("BackImage", typeof(byte[]));
				this.StretchMode = (ImageStretchMode) info.GetValue("StretchMode", typeof(ImageStretchMode));

				this.Watermark = (byte[]) info.GetValue("Watermark", typeof(byte[]));
				this.WatermarkAlignment = (ContentAlignment) info.GetValue("WatermarkAlignment", typeof(ContentAlignment));
			}

			#endregion
		}

		#endregion
	}


	#region TaskPaneInfoConverter

	/// <summary>
	/// A custom TypeConverter used to help convert TaskPaneInfo from 
	/// one Type to another
	/// </summary>
	internal class TaskPaneInfoConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is TaskPaneInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	#endregion

	#endregion


	#region TaskItemInfo Class

	/// <summary>
	/// A class that contains system defined settings for TaskItems
	/// </summary>
	public class TaskItemInfo
	{
		#region Class Data
		
		/// <summary>
		/// The amount of space around the text along each side of 
		/// the TaskItem
		/// </summary>
		private Padding padding; 

		/// <summary>
		/// The amount of space between individual TaskItems 
		/// along each side of the TaskItem
		/// </summary>
		private Margin margin;

		/// <summary>
		/// The Color of the text displayed in the TaskItem
		/// </summary>
		private Color linkNormal;

		/// <summary>
		/// The Color of the text displayed in the TaskItem when 
		/// highlighted
		/// </summary>
		private Color linkHot;

		/// <summary>
		/// The decoration to be used on the text while in a highlighted state
		/// </summary>
		private FontStyle fontDecoration;

		/// <summary>
		/// The TaskItem that owns this TaskItemInfo
		/// </summary>
		private TaskItem owner;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TaskLinkInfo class with default settings
		/// </summary>
		public TaskItemInfo()
		{
			// set padding values
			this.padding = new Padding(6, 0, 4, 0);

			// set margin values
			this.margin = new Margin(0, 4, 0, 0);

			// set text values
			this.linkNormal = SystemColors.ControlText;
			this.linkHot = SystemColors.ControlText;

			this.fontDecoration = FontStyle.Underline;

			this.owner = null;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Forces the use of default values
		/// </summary>
		public void SetDefaultValues()
		{
			// set padding values
			this.padding.Left = 6;
			this.padding.Top = 0;
			this.padding.Right = 4;
			this.padding.Bottom = 0;

			// set margin values
			this.margin.Left = 0;
			this.margin.Top = 4;
			this.margin.Right = 0;
			this.margin.Bottom = 0;

			// set text values
			this.linkNormal = SystemColors.ControlText;
			this.linkHot = SystemColors.HotTrack;

			this.fontDecoration = FontStyle.Underline;
		}


		/// <summary>
		/// Forces the use of default empty values
		/// </summary>
		public void SetDefaultEmptyValues()
		{
			this.padding = Padding.Empty;
			this.margin = Margin.Empty;
			this.linkNormal = Color.Empty;
			this.linkHot = Color.Empty;
			this.fontDecoration = FontStyle.Underline;
		}

		#endregion


		#region Properties

		#region Margin

		/// <summary>
		/// Gets or sets the amount of space between individual TaskItems 
		/// along each side of the TaskItem
		/// </summary>
		[Description("The amount of space between individual TaskItems along each side of the TaskItem")]
		public Margin Margin
		{
			get
			{
				return this.margin;
			}

			set
			{
				if (this.margin != value)
				{
					this.margin = value;

					if (this.TaskItem != null)
					{
						this.TaskItem.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Margin property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the Margin property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeMargin()
		{
			return this.Margin != Margin.Empty;
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets or sets the amount of space around the text along each 
		/// side of the TaskItem
		/// </summary>
		[Description("The amount of space around the text along each side of the TaskItem")]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				if (this.padding != value)
				{
					this.padding = value;

					if (this.TaskItem != null)
					{
						this.TaskItem.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the Padding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the Padding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}

		#endregion

		#region Text

		/// <summary>
		/// Gets or sets the foreground color of a normal link
		/// </summary>
		[Description("The foreground color of a normal link")]
		public Color LinkColor
		{
			get
			{
				return this.linkNormal;
			}

			set
			{
				if (this.linkNormal != value)
				{
					this.linkNormal = value;

					if (this.TaskItem != null)
					{
						this.TaskItem.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the LinkColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the LinkColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeLinkColor()
		{
			return this.LinkColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the foreground color of a highlighted link
		/// </summary>
		[Description("The foreground color of a highlighted link")]
		public Color HotLinkColor
		{
			get
			{
				return this.linkHot;
			}

			set
			{
				if (this.linkHot != value)
				{
					this.linkHot = value;

					if (this.TaskItem != null)
					{
						this.TaskItem.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the HotLinkColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the HotLinkColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeHotLinkColor()
		{
			return this.HotLinkColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the font decoration of a link
		/// </summary>
		[DefaultValue(FontStyle.Underline),
		Description("")]
		public FontStyle FontDecoration
		{
			get
			{
				return this.fontDecoration;
			}

			set
			{
				if (!Enum.IsDefined(typeof(FontStyle), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(FontStyle));
				}

				if (this.fontDecoration != value)
				{
					this.fontDecoration = value;

					if (this.TaskItem != null)
					{
						this.TaskItem.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region TaskItem

		/// <summary>
		/// Gets or sets the TaskItem the TaskItemInfo belongs to
		/// </summary>
		protected internal TaskItem TaskItem
		{
			get
			{
				return this.owner;
			}
			
			set
			{
				this.owner = value;
			}
		}

		#endregion

		#endregion


		#region TaskItemInfoSurrogate

		/// <summary>
		/// A class that is serialized instead of a TaskItemInfo (as 
		/// TaskItemInfos contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class TaskItemInfoSurrogate : ISerializable
		{
			#region Class Data
			
			/// <summary>
			/// See TaskItemInfo.Padding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding Padding; 
			
			/// <summary>
			/// See TaskItemInfo.Margin.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Margin Margin;
			
			/// <summary>
			/// See TaskItemInfo.LinkColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string LinkNormal;
			
			/// <summary>
			/// See TaskItemInfo.HotLinkColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string LinkHot;
			
			/// <summary>
			/// See TaskItemInfo.FontDecoration.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public FontStyle FontDecoration;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the TaskItemInfoSurrogate class with default settings
			/// </summary>
			public TaskItemInfoSurrogate()
			{
				this.Padding = Padding.Empty;
				this.Margin = Margin.Empty;

				this.LinkNormal = ThemeManager.ConvertColorToString(Color.Empty);
				this.LinkHot = ThemeManager.ConvertColorToString(Color.Empty);

				this.FontDecoration = FontStyle.Regular;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the TaskItemInfoSurrogate with data that is to be 
			/// serialized from the specified TaskItemInfo
			/// </summary>
			/// <param name="taskItemInfo">The TaskItemInfo that contains the data 
			/// to be serialized</param>
			public void Load(TaskItemInfo taskItemInfo)
			{
				this.Padding = taskItemInfo.Padding;
				this.Margin = taskItemInfo.Margin;

				this.LinkNormal = ThemeManager.ConvertColorToString(taskItemInfo.LinkColor);
				this.LinkHot = ThemeManager.ConvertColorToString(taskItemInfo.HotLinkColor);

				this.FontDecoration = taskItemInfo.FontDecoration;
			}


			/// <summary>
			/// Returns a TaskItemInfo that contains the deserialized TaskItemInfoSurrogate data
			/// </summary>
			/// <returns>A TaskItemInfo that contains the deserialized TaskItemInfoSurrogate data</returns>
			public TaskItemInfo Save()
			{
				TaskItemInfo taskItemInfo = new TaskItemInfo();

				taskItemInfo.Padding = this.Padding;
				taskItemInfo.Margin = this.Margin;

				taskItemInfo.LinkColor = ThemeManager.ConvertStringToColor(this.LinkNormal);
				taskItemInfo.HotLinkColor = ThemeManager.ConvertStringToColor(this.LinkHot);

				taskItemInfo.FontDecoration = this.FontDecoration;
				
				return taskItemInfo;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the TaskItemInfoSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("Padding", this.Padding);
				info.AddValue("Margin", this.Margin);

				info.AddValue("LinkNormal", this.LinkNormal);
				info.AddValue("LinkHot", this.LinkHot);

				info.AddValue("FontDecoration", this.FontDecoration);
			}


			/// <summary>
			/// Initializes a new instance of the TaskItemInfoSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the TaskItemInfoSurrogate</param>
			/// <param name="context">The source from which the TaskItemInfoSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskItemInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");
				
				this.Padding = (Padding) info.GetValue("Padding", typeof(Padding));
				this.Margin = (Margin) info.GetValue("Margin", typeof(Margin));
				
				this.LinkNormal = info.GetString("LinkNormal");
				this.LinkHot = info.GetString("LinkHot");

				this.FontDecoration = (FontStyle) info.GetValue("FontDecoration", typeof(FontStyle));
			}

			#endregion
		}

		#endregion
	}


	#region TaskItemInfoConverter

	/// <summary>
	/// A custom TypeConverter used to help convert TaskItemInfo from 
	/// one Type to another
	/// </summary>
	internal class TaskItemInfoConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is TaskItemInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	#endregion

	#endregion


	#region ExpandoInfo Class

	/// <summary>
	/// A class that contains system defined settings for Expandos
	/// </summary>
	public class ExpandoInfo : IDisposable
	{
		#region Class Data

		/// <summary>
		/// The background Color of an Expando that is a special group
		/// </summary>
		private Color specialBackColor;
		
		/// <summary>
		/// The background Color of an Expando that is a normal group
		/// </summary>
		private Color normalBackColor;

		/// <summary>
		/// The width of the Border along each edge of an Expando that 
		/// is a special group
		/// </summary>
		private Border specialBorder;
		
		/// <summary>
		/// The width of the Border along each edge of an Expando that 
		/// is a normal group
		/// </summary>
		private Border normalBorder;
		
		/// <summary>
		/// The Color of the Border an Expando that is a special group
		/// </summary>
		private Color specialBorderColor;
		
		/// <summary>
		/// The Color of the Border an Expando that is a normal group
		/// </summary>
		private Color normalBorderColor;

		/// <summary>
		/// The amount of space between the Border and items along 
		/// each edge of an Expando that is a special group
		/// </summary>
		private Padding specialPadding;
		
		/// <summary>
		/// The amount of space between the Border and items along 
		/// each edge of an Expando that is a normal group
		/// </summary>
		private Padding normalPadding;

		/// <summary>
		/// The alignment of the Image that is to be used as a watermark
		/// </summary>
		private ContentAlignment watermarkAlignment;

		/// <summary>
		/// The background image used for the content area of a special Expando
		/// </summary>
		private Image specialBackImage;

		/// <summary>
		/// The background image used for the content area of a normal Expando
		/// </summary>
		private Image normalBackImage;

		/// <summary>
		/// The Expando that the ExpandoInfo belongs to
		/// </summary>
		private Expando owner;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ExpandoInfo class with default settings
		/// </summary>
		public ExpandoInfo()
		{
			// set background color values
			this.specialBackColor = Color.Transparent;
			this.normalBackColor = Color.Transparent;

			// set border values
			this.specialBorder = new Border(1, 0, 1, 1);
			this.specialBorderColor = Color.Transparent;

			this.normalBorder = new Border(1, 0, 1, 1);
			this.normalBorderColor = Color.Transparent;

			// set padding values
			this.specialPadding = new Padding(12, 10, 12, 10);
			this.normalPadding = new Padding(12, 10, 12, 10);

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;

			this.owner = null;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Forces the use of default values
		/// </summary>
		public void SetDefaultValues()
		{
			// set background color values
			this.specialBackColor = SystemColors.Window;
			this.normalBackColor = SystemColors.Window;

			// set border values
			this.specialBorder.Left = 1;
			this.specialBorder.Top = 0;
			this.specialBorder.Right = 1;
			this.specialBorder.Bottom = 1;

            this.specialBorderColor = SystemColors.Highlight;

			this.normalBorder.Left = 1;
			this.normalBorder.Top = 0;
			this.normalBorder.Right = 1;
			this.normalBorder.Bottom = 1;

            this.normalBorderColor = SystemColors.ActiveCaption;

			// set padding values
			this.specialPadding.Left = 12;
			this.specialPadding.Top = 10;
			this.specialPadding.Right = 12;
			this.specialPadding.Bottom = 10;
			
			this.normalPadding.Left = 12;
			this.normalPadding.Top = 10;
			this.normalPadding.Right = 12;
			this.normalPadding.Bottom = 10;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;
		}


		/// <summary>
		/// Forces the use of default empty values
		/// </summary>
		public void SetDefaultEmptyValues()
		{
			// set background color values
			this.specialBackColor = Color.Empty;
			this.normalBackColor = Color.Empty;

			// set border values
			this.specialBorder = Border.Empty;
			this.specialBorderColor = Color.Empty;

			this.normalBorder = Border.Empty;
			this.normalBorderColor = Color.Empty;

			// set padding values
			this.specialPadding = Padding.Empty;
			this.normalPadding = Padding.Empty;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;
		}


		/// <summary>
		/// Releases all resources used by the ExpandoInfo
		/// </summary>
		public void Dispose()
		{
			if (this.specialBackImage != null)
			{
				this.specialBackImage.Dispose();
				this.specialBackImage = null;
			}

			if (this.normalBackImage != null)
			{
				this.normalBackImage.Dispose();
				this.normalBackImage = null;
			}
		}

		#endregion


		#region Properties

		#region Background

		/// <summary>
		/// Gets or sets the background color of a special expando
		/// </summary>
		[Description("The background color of a special Expando")]
		public Color SpecialBackColor
		{
			get
			{
				return this.specialBackColor;
			}

			set
			{
				if (this.specialBackColor != value)
				{
					this.specialBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBackColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBackColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBackColor()
		{
			return this.SpecialBackColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the background color of a normal expando
		/// </summary>
		[Description("The background color of a normal Expando")]
		public Color NormalBackColor
		{
			get
			{
				return this.normalBackColor;
			}

			set
			{
				if (this.normalBackColor != value)
				{
					this.normalBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBackColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBackColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBackColor()
		{
			return this.NormalBackColor != Color.Empty;
		}

		
		/// <summary>
		/// Gets or sets the alignment for the expando's background image
		/// </summary>
		[DefaultValue(ContentAlignment.BottomRight), 
		Description("The alignment for the expando's background image")]
		public ContentAlignment WatermarkAlignment
		{
			get
			{
				return this.watermarkAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.watermarkAlignment != value)
				{
					this.watermarkAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a special expando's background image
		/// </summary>
		[DefaultValue(null), 
		Description("")]
		public Image SpecialBackImage
		{
			get
			{
				return this.specialBackImage;
			}

			set
			{
				if (this.specialBackImage != value)
				{
					this.specialBackImage = value;
				}
			}
		}


		/// <summary>
		/// Gets or sets a normal expando's background image
		/// </summary>
		[DefaultValue(null), 
		Description("")]
		public Image NormalBackImage
		{
			get
			{
				return this.normalBackImage;
			}

			set
			{
				if (this.normalBackImage != value)
				{
					this.normalBackImage = value;
				}
			}
		}

		#endregion

		#region Border

		/// <summary>
		/// Gets or sets the border for a special expando
		/// </summary>
		[Description("The width of the Border along each side of a special Expando")]
		public Border SpecialBorder
		{
			get
			{
				return this.specialBorder;
			}

			set
			{
				if (this.specialBorder != value)
				{
					this.specialBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBorder property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBorder property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBorder()
		{
			return this.SpecialBorder != Border.Empty;
		}


		/// <summary>
		/// Gets or sets the border for a normal expando
		/// </summary>
		[Description("The width of the Border along each side of a normal Expando")]
		public Border NormalBorder
		{
			get
			{
				return this.normalBorder;
			}

			set
			{
				if (this.normalBorder != value)
				{
					this.normalBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBorder property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBorder property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBorder()
		{
			return this.NormalBorder != Border.Empty;
		}


		/// <summary>
		/// Gets or sets the border color for a special expando
		/// </summary>
		[Description("The border color for a special Expando")]
		public Color SpecialBorderColor
		{
			get
			{
				return this.specialBorderColor;
			}

			set
			{
				if (this.specialBorderColor != value)
				{
					this.specialBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBorderColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBorderColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBorderColor()
		{
			return this.SpecialBorderColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the border color for a normal expando
		/// </summary>
		[Description("The border color for a normal Expando")]
		public Color NormalBorderColor
		{
			get
			{
				return this.normalBorderColor;
			}

			set
			{
				if (this.normalBorderColor != value)
				{
					this.normalBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBorderColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBorderColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBorderColor()
		{
			return this.NormalBorderColor != Color.Empty;
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets or sets the padding value for a special expando
		/// </summary>
		[Description("The amount of space between the border and items along each side of a special Expando")]
		public Padding SpecialPadding
		{
			get
			{
				return this.specialPadding;
			}

			set
			{
				if (this.specialPadding != value)
				{
					this.specialPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialPadding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialPadding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialPadding()
		{
			return this.SpecialPadding != Padding.Empty;
		}
		

		/// <summary>
		/// Gets or sets the padding value for a normal expando
		/// </summary>
		[Description("The amount of space between the border and items along each side of a normal Expando")]
		public Padding NormalPadding
		{
			get
			{
				return this.normalPadding;
			}

			set
			{
				if (this.normalPadding != value)
				{
					this.normalPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalPadding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalPadding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalPadding()
		{
			return this.NormalPadding != Padding.Empty;
		}

		#endregion

		#region Expando

		/// <summary>
		/// Gets or sets the Expando that the ExpandoInfo belongs to
		/// </summary>
		protected internal Expando Expando
		{
			get
			{
				return this.owner;
			}

			set
			{
				this.owner = value;
			}
		}

		#endregion

		#endregion


		#region ExpandoInfoSurrogate

		/// <summary>
		/// A class that is serialized instead of an ExpandoInfo (as 
		/// ExpandoInfos contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class ExpandoInfoSurrogate : ISerializable
		{
			#region Class Data
			
			/// <summary>
			/// See ExpandoInfo.SpecialBackColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialBackColor;
			
			/// <summary>
			/// See ExpandoInfo.NormalBackColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalBackColor;
			
			/// <summary>
			/// See ExpandoInfo.SpecialBorder.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Border SpecialBorder;
			
			/// <summary>
			/// See ExpandoInfo.NormalBorder.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Border NormalBorder;
			
			/// <summary>
			/// See ExpandoInfo.SpecialBorderColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialBorderColor;
			
			/// <summary>
			/// See ExpandoInfo.NormalBorderColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalBorderColor;
			
			/// <summary>
			/// See ExpandoInfo.SpecialPadding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding SpecialPadding;
			
			/// <summary>
			/// See ExpandoInfo.NormalPadding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding NormalPadding;
			
			/// <summary>
			/// See ExpandoInfo.SpecialBackImage.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialBackImage;
			
			/// <summary>
			/// See ExpandoInfo.NormalBackImage.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalBackImage;
			
			/// <summary>
			/// See ExpandoInfo.WatermarkAlignment.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public ContentAlignment WatermarkAlignment;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the ExpandoInfoSurrogate class with default settings
			/// </summary>
			public ExpandoInfoSurrogate()
			{
				this.SpecialBackColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBackColor = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialBorder = Border.Empty;
				this.NormalBorder = Border.Empty;

				this.SpecialBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				
				this.SpecialPadding = Padding.Empty;
				this.NormalPadding = Padding.Empty;

				this.SpecialBackImage = new byte[0];
				this.NormalBackImage = new byte[0];

				this.WatermarkAlignment = ContentAlignment.BottomRight;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the ExpandoInfoSurrogate with data that is to be 
			/// serialized from the specified ExpandoInfo
			/// </summary>
			/// <param name="expandoInfo">The ExpandoInfo that contains the data 
			/// to be serialized</param>
			public void Load(ExpandoInfo expandoInfo)
			{
				this.SpecialBackColor = ThemeManager.ConvertColorToString(expandoInfo.SpecialBackColor);
				this.NormalBackColor =ThemeManager.ConvertColorToString( expandoInfo.NormalBackColor);

				this.SpecialBorder = expandoInfo.SpecialBorder;
				this.NormalBorder = expandoInfo.NormalBorder;

				this.SpecialBorderColor = ThemeManager.ConvertColorToString(expandoInfo.SpecialBorderColor);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(expandoInfo.NormalBorderColor);

				this.SpecialPadding = expandoInfo.SpecialPadding;
				this.NormalPadding = expandoInfo.NormalPadding;

				this.SpecialBackImage = ThemeManager.ConvertImageToByteArray(expandoInfo.SpecialBackImage);
				this.NormalBackImage = ThemeManager.ConvertImageToByteArray(expandoInfo.NormalBackImage);

				this.WatermarkAlignment = expandoInfo.WatermarkAlignment;
			}


			/// <summary>
			/// Returns an ExpandoInfo that contains the deserialized ExpandoInfoSurrogate data
			/// </summary>
			/// <returns>An ExpandoInfo that contains the deserialized ExpandoInfoSurrogate data</returns>
			public ExpandoInfo Save()
			{
				ExpandoInfo expandoInfo = new ExpandoInfo();

				expandoInfo.SpecialBackColor = ThemeManager.ConvertStringToColor(this.SpecialBackColor);
				expandoInfo.NormalBackColor = ThemeManager.ConvertStringToColor(this.NormalBackColor);

				expandoInfo.SpecialBorder = this.SpecialBorder;
				expandoInfo.NormalBorder = this.NormalBorder;

				expandoInfo.SpecialBorderColor = ThemeManager.ConvertStringToColor(this.SpecialBorderColor);
				expandoInfo.NormalBorderColor = ThemeManager.ConvertStringToColor(this.NormalBorderColor);
				
				expandoInfo.SpecialPadding = this.SpecialPadding;
				expandoInfo.NormalPadding = this.NormalPadding;

				expandoInfo.SpecialBackImage = ThemeManager.ConvertByteArrayToImage(this.SpecialBackImage);
				expandoInfo.NormalBackImage = ThemeManager.ConvertByteArrayToImage(this.NormalBackImage);
				
				expandoInfo.WatermarkAlignment = this.WatermarkAlignment;
				
				return expandoInfo;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the ExpandoInfoSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("SpecialBackColor", this.SpecialBackColor);
				info.AddValue("NormalBackColor", this.NormalBackColor);
				
				info.AddValue("SpecialBorder", this.SpecialBorder);
				info.AddValue("NormalBorder", this.NormalBorder);
				
				info.AddValue("SpecialBorderColor", this.SpecialBorderColor);
				info.AddValue("NormalBorderColor", this.NormalBorderColor);
				
				info.AddValue("SpecialPadding", this.SpecialPadding);
				info.AddValue("NormalPadding", this.NormalPadding);
				
				info.AddValue("SpecialBackImage", this.SpecialBackImage);
				info.AddValue("NormalBackImage", this.NormalBackImage);
				
				info.AddValue("WatermarkAlignment", this.WatermarkAlignment);
			}


			/// <summary>
			/// Initializes a new instance of the ExpandoInfoSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the ExpandoInfoSurrogate</param>
			/// <param name="context">The source from which the ExpandoInfoSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected ExpandoInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.SpecialBackColor = info.GetString("SpecialBackColor");
				this.NormalBackColor = info.GetString("NormalBackColor");

				this.SpecialBorder = (Border) info.GetValue("SpecialBorder", typeof(Border));
				this.NormalBorder = (Border) info.GetValue("NormalBorder", typeof(Border));

				this.SpecialBorderColor = info.GetString("SpecialBorderColor");
				this.NormalBorderColor = info.GetString("NormalBorderColor");

				this.SpecialPadding = (Padding) info.GetValue("SpecialPadding", typeof(Padding));
				this.NormalPadding = (Padding) info.GetValue("NormalPadding", typeof(Padding));

				this.SpecialBackImage = (byte[]) info.GetValue("SpecialBackImage", typeof(byte[]));
				this.NormalBackImage = (byte[]) info.GetValue("NormalBackImage", typeof(byte[]));

				this.WatermarkAlignment = (ContentAlignment) info.GetValue("WatermarkAlignment", typeof(ContentAlignment));
			}

			#endregion
		}

		#endregion
	}


	#region ExpandoInfoConverter

	/// <summary>
	/// A custom TypeConverter used to help convert ExpandoInfos from 
	/// one Type to another
	/// </summary>
	internal class ExpandoInfoConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is ExpandoInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			// set the order in which the properties appear 
			// in the property window
			
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(ExpandoInfo), attributes);

			string[] s = new string[9];
			s[0] = "NormalBackColor";
			s[1] = "SpecialBackColor";
			s[2] = "NormalBorder";
			s[3] = "SpecialBorder";
			s[4] = "NormalBorderColor";
			s[5] = "SpecialBorderColor";
			s[6] = "NormalPadding";
			s[7] = "SpecialPadding";
			s[8] = "WatermarkAlignment";

			return collection.Sort(s);
		}
	}

	#endregion

	#endregion


	#region HeaderInfo Class

	/// <summary>
	/// A class that contains system defined settings for an Expando's 
	/// header section
	/// </summary>
	public class HeaderInfo : IDisposable
	{
		#region Class Data
		
		/// <summary>
		/// The Font used to draw the text on the title bar
		/// </summary>
		private Font titleFont;

		/// <summary>
		/// The Margin around the header
		/// </summary>
		private int margin;

		/// <summary>
		/// The Image used as the title bar's background for a special Expando
		/// </summary>
		private Image specialBackImage;

		/// <summary>
		/// The Image used as the title bar's background for a normal Expando
		/// </summary>
		private Image normalBackImage;

		/// <summary>
		///  The width of the Image used as the title bar's background
		/// </summary>
		private int backImageWidth;

		/// <summary>
		/// The height of the Image used as the title bar's background
		/// </summary>
		private int backImageHeight;

		/// <summary>
		/// The Color of the text on the title bar for a special Expando
		/// </summary>
		private Color specialTitle;
		
		/// <summary>
		/// The Color of the text on the title bar for a normal Expando
		/// </summary>
		private Color normalTitle;

		/// <summary>
		/// The Color of the text on the title bar for a special Expando 
		/// when highlighted
		/// </summary>
		private Color specialTitleHot;

		/// <summary>
		/// The Color of the text on the title bar for a normal Expando 
		/// when highlighted
		/// </summary>
		private Color normalTitleHot;

		/// <summary>
		/// The alignment of the text on the title bar for a special Expando
		/// </summary>
		private ContentAlignment specialAlignment;

		/// <summary>
		/// The alignment of the text on the title bar for a normal Expando
		/// </summary>
		private ContentAlignment normalAlignment;
		
		/// <summary>
		/// The amount of space between the border and items along 
		/// each edge of the title bar for a special Expando
		/// </summary>
		private Padding specialPadding;

		/// <summary>
		/// The amount of space between the border and items along 
		/// each edge of the title bar for a normal Expando
		/// </summary>
		private Padding normalPadding;

		/// <summary>
		/// The width of the Border along each edge of the title bar 
		/// for a special Expando
		/// </summary>
		private Border specialBorder;

		/// <summary>
		/// The width of the Border along each edge of the title bar 
		/// for a normal Expando
		/// </summary>
		private Border normalBorder;

		/// <summary>
		/// The Color of the title bar's Border for a special Expando
		/// </summary>
		private Color specialBorderColor;

		/// <summary>
		/// The Color of the title bar's Border for a normal Expando
		/// </summary>
		private Color normalBorderColor;

		/// <summary>
		/// The Color of the title bar's background for a special Expando
		/// </summary>
		private Color specialBackColor;

		/// <summary>
		/// The Color of the title bar's background for a normal Expando
		/// </summary>
		private Color normalBackColor;

		/// <summary>
		/// The Image that is used as a collapse arrow on the title bar 
		/// for a special Expando
		/// </summary>
		private Image specialArrowUp;
		
		/// <summary>
		/// The Image that is used as a collapse arrow on the title bar 
		/// for a special Expando when highlighted
		/// </summary>
		private Image specialArrowUpHot;
		
		/// <summary>
		/// The Image that is used as an expand arrow on the title bar 
		/// for a special Expando
		/// </summary>
		private Image specialArrowDown;
		
		/// <summary>
		/// The Image that is used as an expand arrow on the title bar 
		/// for a special Expando when highlighted
		/// </summary>
		private Image specialArrowDownHot;
		
		/// <summary>
		/// The Image that is used as a collapse arrow on the title bar 
		/// for a normal Expando
		/// </summary>
		private Image normalArrowUp;
		
		/// <summary>
		/// The Image that is used as a collapse arrow on the title bar 
		/// for a normal Expando when highlighted
		/// </summary>
		private Image normalArrowUpHot;
		
		/// <summary>
		/// The Image that is used as an expand arrow on the title bar 
		/// for a normal Expando
		/// </summary>
		private Image normalArrowDown;
		
		/// <summary>
		/// The Image that is used as an expand arrow on the title bar 
		/// for a normal Expando when highlighted
		/// </summary>
		private Image normalArrowDownHot;

		/// <summary>
		/// Specifies whether the title bar should use a gradient fill
		/// </summary>
		private bool useTitleGradient;

		/// <summary>
		/// The start Color of a title bar's gradient fill for a special 
		/// Expando
		/// </summary>
		private Color specialGradientStartColor;

		/// <summary>
		/// The end Color of a title bar's gradient fill for a special 
		/// Expando
		/// </summary>
		private Color specialGradientEndColor;

		/// <summary>
		/// The start Color of a title bar's gradient fill for a normal 
		/// Expando
		/// </summary>
		private Color normalGradientStartColor;

		/// <summary>
		/// The end Color of a title bar's gradient fill for a normal 
		/// Expando
		/// </summary>
		private Color normalGradientEndColor;

		/// <summary>
		/// How far along the title bar the gradient starts
		/// </summary>
		private float gradientOffset;

		/// <summary>
		/// The radius of the corners on the title bar
		/// </summary>
		private int titleRadius;

		/// <summary>
		/// The Expando that the HeaderInfo belongs to
		/// </summary>
		private Expando owner;

		/// <summary>
		/// 
		/// </summary>
		private bool rightToLeft;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the HeaderInfo class with default settings
		/// </summary>
		public HeaderInfo()
		{
			// work out the default font name for the user's os.
			// this ignores other fonts that may be specified - need 
			// to change parser to get font names
			if (Environment.OSVersion.Version.Major >= 5)
			{
				// Win2k, XP, Server 2003
				this.titleFont = new Font("Tahoma", 8.25f, FontStyle.Bold);
			}
			else
			{
				// Win9x, ME, NT
				this.titleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
			}

			this.margin = 15;

			// set title colors and alignment
			this.specialTitle = Color.Transparent;
			this.specialTitleHot = Color.Transparent;
			
			this.normalTitle = Color.Transparent;
			this.normalTitleHot = Color.Transparent;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

			// set padding values
			this.specialPadding = new Padding(10, 0, 1, 0);
			this.normalPadding = new Padding(10, 0, 1, 0);

			// set border values
			this.specialBorder = new Border(2, 2, 2, 0);
			this.specialBorderColor = Color.Transparent;

			this.normalBorder = new Border(2, 2, 2, 0);
			this.normalBorderColor = Color.Transparent;
			
			this.specialBackColor = Color.Transparent;
			this.normalBackColor = Color.Transparent;

			// set background image values
			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = -1;
			this.backImageHeight = -1;

			// set arrow values
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.White;
			this.specialGradientEndColor = SystemColors.Highlight;
			this.normalGradientStartColor = Color.White;
			this.normalGradientEndColor = SystemColors.Highlight;
			this.gradientOffset = 0.5f;
			this.titleRadius = 5;

			this.owner = null;
			this.rightToLeft = false;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Forces the use of default values
		/// </summary>
		public void SetDefaultValues()
		{
			// work out the default font name for the user's os
            /*
			if (Environment.OSVersion.Version.Major >= 5)
			{
				// Win2k, XP, Server 2003
				this.titleFont = new Font("Tahoma", 8.25f, FontStyle.Bold);
			}
			else
			{
				// Win9x, ME, NT
				this.titleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
			}
            */
            Font ft = SystemFonts.MenuFont;
            this.titleFont = new Font(ft.FontFamily.Name, ft.Size, FontStyle.Bold);
			this.margin = 15;

			// set title colors and alignment
			this.specialTitle = SystemColors.HighlightText;
			this.specialTitleHot = SystemColors.HighlightText;

            this.normalTitle = SystemColors.ActiveCaptionText;
            this.normalTitleHot = SystemColors.ActiveCaptionText;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

			// set padding values
			this.specialPadding.Left = 10;
			this.specialPadding.Top = 0;
			this.specialPadding.Right = 1;
			this.specialPadding.Bottom = 0;

			this.normalPadding.Left = 10;
			this.normalPadding.Top = 0;
			this.normalPadding.Right = 1;
			this.normalPadding.Bottom = 0;

			// set border values
			this.specialBorder.Left = 2;
			this.specialBorder.Top = 2;
			this.specialBorder.Right = 2;
			this.specialBorder.Bottom = 0;

            this.specialBorderColor = SystemColors.Highlight;
			this.specialBackColor = SystemColors.Highlight;

			this.normalBorder.Left = 2;
			this.normalBorder.Top = 2;
			this.normalBorder.Right = 2;
			this.normalBorder.Bottom = 0;

            this.normalBorderColor = SystemColors.ActiveCaption;
            this.normalBackColor = SystemColors.ActiveCaption;

			// set background image values
			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = 186;
			this.backImageHeight = 25;

			// set arrow values
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.White;
			this.specialGradientEndColor = SystemColors.Highlight;
			this.normalGradientStartColor = Color.White;
			this.normalGradientEndColor = SystemColors.Highlight;
			this.gradientOffset = 0.5f;
			this.titleRadius = 2;

			this.rightToLeft = false;
		}
		

		/// <summary>
		/// Forces the use of default empty values
		/// </summary>
		public void SetDefaultEmptyValues()
		{
			// work out the default font name for the user's os
			this.titleFont = null;

			this.margin = 15;

			// set title colors and alignment
			this.specialTitle = Color.Empty;
			this.specialTitleHot = Color.Empty;
			
			this.normalTitle = Color.Empty;
			this.normalTitleHot = Color.Empty;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

			// set padding values
			this.specialPadding = Padding.Empty;
			this.normalPadding = Padding.Empty;

			// set border values
			this.specialBorder = Border.Empty;
			this.specialBorderColor = Color.Empty;
			this.specialBackColor = Color.Empty;

			this.normalBorder = Border.Empty;
			this.normalBorderColor = Color.Empty;
			this.normalBackColor = Color.Empty;

			// set background image values
			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = 186;
			this.backImageHeight = 25;

			// set arrow values
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.Empty;
			this.specialGradientEndColor = Color.Empty;
			this.normalGradientStartColor = Color.Empty;
			this.normalGradientEndColor = Color.Empty;
			this.gradientOffset = 0.5f;
			this.titleRadius = 2;

			this.rightToLeft = false;
		}


		/// <summary>
		/// Releases all resources used by the HeaderInfo
		/// </summary>
		public void Dispose()
		{
			if (this.specialBackImage != null)
			{
				this.specialBackImage.Dispose();
				this.specialBackImage = null;
			}

			if (this.normalBackImage != null)
			{
				this.normalBackImage.Dispose();
				this.normalBackImage = null;
			}


			if (this.specialArrowUp != null)
			{
				this.specialArrowUp.Dispose();
				this.specialArrowUp = null;
			}

			if (this.specialArrowUpHot != null)
			{
				this.specialArrowUpHot.Dispose();
				this.specialArrowUpHot = null;
			}

			if (this.specialArrowDown != null)
			{
				this.specialArrowDown.Dispose();
				this.specialArrowDown = null;
			}

			if (this.specialArrowDownHot != null)
			{
				this.specialArrowDownHot.Dispose();
				this.specialArrowDownHot = null;
			}
			
			if (this.normalArrowUp != null)
			{
				this.normalArrowUp.Dispose();
				this.normalArrowUp = null;
			}

			if (this.normalArrowUpHot != null)
			{
				this.normalArrowUpHot.Dispose();
				this.normalArrowUpHot = null;
			}

			if (this.normalArrowDown != null)
			{
				this.normalArrowDown.Dispose();
				this.normalArrowDown = null;
			}

			if (this.normalArrowDownHot != null)
			{
				this.normalArrowDownHot.Dispose();
				this.normalArrowDownHot = null;
			}
		}

		#endregion


		#region Properties

		#region Border

		/// <summary>
		/// Gets or sets the border value for a special header
		/// </summary>
		[Description("The width of the border along each side of a special Expando's Title Bar")]
		public Border SpecialBorder
		{
			get
			{
				return this.specialBorder;
			}

			set
			{
				if (this.specialBorder != value)
				{
					this.specialBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBorder property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBorder property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBorder()
		{
			return this.SpecialBorder != Border.Empty;
		}


		/// <summary>
		/// Gets or sets the border color for a special header
		/// </summary>
		[Description("The border color for a special Expandos titlebar")]
		public Color SpecialBorderColor
		{
			get
			{
				return this.specialBorderColor;
			}

			set
			{
				if (this.specialBorderColor != value)
				{
					this.specialBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBorderColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBorderColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBorderColor()
		{
			return this.SpecialBorderColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the background Color for a special header
		/// </summary>
		[Description("The background Color for a special Expandos titlebar")]
		public Color SpecialBackColor
		{
			get
			{
				return this.specialBackColor;
			}

			set
			{
				if (this.specialBackColor != value)
				{
					this.specialBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialBackColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialBackColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialBackColor()
		{
			return this.SpecialBackColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the border value for a normal header
		/// </summary>
		[Description("The width of the border along each side of a normal Expando's Title Bar")]
		public Border NormalBorder
		{
			get
			{
				return this.normalBorder;
			}

			set
			{
				if (this.normalBorder != value)
				{
					this.normalBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBorder property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBorder property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBorder()
		{
			return this.NormalBorder != Border.Empty;
		}


		/// <summary>
		/// Gets or sets the border color for a normal header
		/// </summary>
		[Description("The border color for a normal Expandos titlebar")]
		public Color NormalBorderColor
		{
			get
			{
				return this.normalBorderColor;
			}

			set
			{
				if (this.normalBorderColor != value)
				{
					this.normalBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBorderColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBorderColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBorderColor()
		{
			return this.NormalBorderColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the background Color for a normal header
		/// </summary>
		[Description("The background Color for a normal Expandos titlebar")]
		public Color NormalBackColor
		{
			get
			{
				return this.normalBackColor;
			}

			set
			{
				if (this.normalBackColor != value)
				{
					this.normalBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalBackColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalBackColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalBackColor()
		{
			return this.NormalBackColor != Color.Empty;
		}

		#endregion

		#region Fonts

		/// <summary>
		/// Gets the Font used to render the header's text
		/// </summary>
		[DefaultValue(null), 
		Description("The Font used to render the titlebar's text")]
		public Font TitleFont
		{
			get
			{
				return this.titleFont;
			}

			set
			{
				if (this.titleFont != value)
				{
					this.titleFont = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the font used to render the header's text. 
		/// </summary>
		protected internal string FontName
		{
			get
			{
				return this.TitleFont.Name;
			}

			set
			{
				this.TitleFont = new Font(value, this.TitleFont.SizeInPoints, this.TitleFont.Style);
			}
		}


		/// <summary>
		/// Gets or sets the size of the font used to render the header's text. 
		/// </summary>
		protected internal float FontSize
		{
			get
			{
				return this.TitleFont.SizeInPoints;
			}

			set
			{
				this.TitleFont = new Font(this.TitleFont.Name, value, this.TitleFont.Style);
			}
		}


		/// <summary>
		/// Gets or sets the weight of the font used to render the header's text. 
		/// </summary>
		protected internal FontStyle FontWeight
		{
			get
			{
				return this.TitleFont.Style;
			}

			set
			{
				value |= this.TitleFont.Style;
				
				this.TitleFont = new Font(this.TitleFont.Name, this.TitleFont.SizeInPoints, value);
			}
		}
		
		
		/// <summary>
		/// Gets or sets the style of the Font used to render the header's text. 
		/// </summary>
		protected internal FontStyle FontStyle
		{
			get
			{
				return this.TitleFont.Style;
			}

			set
			{
				value |= this.TitleFont.Style;
				
				this.TitleFont = new Font(this.TitleFont.Name, this.TitleFont.SizeInPoints, value);
			}
		}

		#endregion

		#region Images

		/// <summary>
		/// Gets or sets the background image for a special header
		/// </summary>
		[DefaultValue(null), 
		Description("The background image for a special titlebar")]
		public Image SpecialBackImage
		{
			get
			{
				return this.specialBackImage;
			}

			set
			{
				if (this.specialBackImage != value)
				{
					this.specialBackImage = value;

					if (value!= null)
					{
						this.backImageWidth = value.Width;
						this.backImageHeight = value.Height;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the background image for a normal header
		/// </summary>
		[DefaultValue(null), 
		Description("The background image for a normal titlebar")]
		public Image NormalBackImage
		{
			get
			{
				return this.normalBackImage;
			}

			set
			{
				if (this.normalBackImage != value)
				{
					this.normalBackImage = value;

					if (value!= null)
					{
						this.backImageWidth = value.Width;
						this.backImageHeight = value.Height;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the width of the header's background image
		/// </summary>
		protected internal int BackImageWidth
		{
			get
			{
				if (this.backImageWidth == -1)
				{
					return 186;
				}
				
				return this.backImageWidth;
			}

			set
			{
				this.backImageWidth = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of the header's background image
		/// </summary>
		protected internal int BackImageHeight
		{
			get
			{
				if (this.backImageHeight < 23)
				{
					return 23;
				}
				
				return this.backImageHeight;
			}

			set
			{
				this.backImageHeight = value;
			}
		}
		
		
		/// <summary>
		/// Gets or sets a special header's collapse arrow image in it's normal state
		/// </summary>
		[DefaultValue(null), 
		Description("A special Expando's collapse arrow image in it's normal state")]
		public Image SpecialArrowUp
		{
			get
			{
				return this.specialArrowUp;
			}

			set
			{
				if (this.specialArrowUp != value)
				{
					this.specialArrowUp = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a special header's collapse arrow image in it's highlighted state
		/// </summary>
		[DefaultValue(null), 
		Description("A special Expando's collapse arrow image in it's highlighted state")]
		public Image SpecialArrowUpHot
		{
			get
			{
				return this.specialArrowUpHot;
			}

			set
			{
				if (this.specialArrowUpHot != value)
				{
					this.specialArrowUpHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a special header's expand arrow image in it's normal state
		/// </summary>
		[DefaultValue(null), 
		Description("A special Expando's expand arrow image in it's normal state")]
		public Image SpecialArrowDown
		{
			get
			{
				return this.specialArrowDown;
			}

			set
			{
				if (this.specialArrowDown != value)
				{
					this.specialArrowDown = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a special header's expend arrow image in it's highlighted state
		/// </summary>
		[DefaultValue(null), 
		Description("A special Expando's expand arrow image in it's highlighted state")]
		public Image SpecialArrowDownHot
		{
			get
			{
				return this.specialArrowDownHot;
			}

			set
			{
				if (this.specialArrowDownHot != value)
				{
					this.specialArrowDownHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}
		
		
		/// <summary>
		/// Gets or sets a normal header's collapse arrow image in it's normal state
		/// </summary>
		[DefaultValue(null), 
		Description("A normal Expando's collapse arrow image in it's normal state")]
		public Image NormalArrowUp
		{
			get
			{
				return this.normalArrowUp;
			}

			set
			{
				if (this.normalArrowUp != value)
				{
					this.normalArrowUp = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a normal header's collapse arrow image in it's highlighted state
		/// </summary>
		[DefaultValue(null), 
		Description("A normal Expando's collapse arrow image in it's highlighted state")]
		public Image NormalArrowUpHot
		{
			get
			{
				return this.normalArrowUpHot;
			}

			set
			{
				if (this.normalArrowUpHot != value)
				{
					this.normalArrowUpHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a normal header's expand arrow image in it's normal state
		/// </summary>
		[DefaultValue(null), 
		Description("A normal Expando's expand arrow image in it's normal state")]
		public Image NormalArrowDown
		{
			get
			{
				return this.normalArrowDown;
			}

			set
			{
				if (this.normalArrowDown != value)
				{
					this.normalArrowDown = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets a normal header's expand arrow image in it's highlighted state
		/// </summary>
		[DefaultValue(null), 
		Description("A normal Expando's expand arrow image in it's highlighted state")]
		public Image NormalArrowDownHot
		{
			get
			{
				return this.normalArrowDownHot;
			}

			set
			{
				if (this.normalArrowDownHot != value)
				{
					this.normalArrowDownHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Sets the arrow images for use when theming is not supported
		/// </summary>
		internal void SetUnthemedArrowImages()
		{
			// get the arrow images resource
			System.Reflection.Assembly myAssembly;
			myAssembly = this.GetType().Assembly;
			ResourceManager myManager = new ResourceManager("ZForge.Controls.ExplorerBar.ExpandoArrows", myAssembly);
				
			// set the arrow images
			this.specialArrowDown = new Bitmap((Image) myManager.GetObject("SPECIALGROUPEXPAND"));
			this.specialArrowDownHot = new Bitmap((Image) myManager.GetObject("SPECIALGROUPEXPANDHOT"));
			this.specialArrowUp = new Bitmap((Image) myManager.GetObject("SPECIALGROUPCOLLAPSE"));
			this.specialArrowUpHot = new Bitmap((Image) myManager.GetObject("SPECIALGROUPCOLLAPSEHOT"));
				
			this.normalArrowDown = new Bitmap((Image) myManager.GetObject("NORMALGROUPEXPAND"));
			this.normalArrowDownHot = new Bitmap((Image) myManager.GetObject("NORMALGROUPEXPANDHOT"));
			this.normalArrowUp = new Bitmap((Image) myManager.GetObject("NORMALGROUPCOLLAPSE"));
			this.normalArrowUpHot = new Bitmap((Image) myManager.GetObject("NORMALGROUPCOLLAPSEHOT"));
		}

		#endregion

		#region Margin

		/// <summary>
		/// Gets or sets the margin around the header
		/// </summary>
		[DefaultValue(15), 
		Description("The margin around the titlebar")]
		public int Margin
		{
			get
			{
				return this.margin;
			}

			set
			{
				if (this.margin != value)
				{
					this.margin = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets or sets the padding for a special header
		/// </summary>
		[Description("The amount of space between the border and items along each side of a special Expandos Title Bar")]
		public Padding SpecialPadding
		{
			get
			{
				return this.specialPadding;
			}

			set
			{
				if (this.specialPadding != value)
				{
					this.specialPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialPadding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialPadding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialPadding()
		{
			return this.SpecialPadding != Padding.Empty;
		}


		/// <summary>
		/// Gets or sets the padding for a normal header
		/// </summary>
		[Description("The amount of space between the border and items along each side of a normal Expandos Title Bar")]
		public Padding NormalPadding
		{
			get
			{
				return this.normalPadding;
			}

			set
			{
				if (this.normalPadding != value)
				{
					this.normalPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalPadding property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalPadding property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalPadding()
		{
			return this.NormalPadding != Padding.Empty;
		}

		#endregion

		#region Title

		/// <summary>
		/// Gets or sets the color of the text displayed in a special 
		/// header in it's normal state
		/// </summary>
		[Description("The color of the text displayed in a special Expandos titlebar in it's normal state")]
		public Color SpecialTitleColor
		{
			get
			{
				return this.specialTitle;
			}

			set
			{
				if (this.specialTitle != value)
				{
					this.specialTitle = value;

					// set the SpecialTitleHotColor as well just in case
					// it isn't/wasn't set during UIFILE parsing
					if (this.SpecialTitleHotColor == Color.Transparent)
					{
						this.SpecialTitleHotColor = value;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialTitleColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialTitleColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialTitleColor()
		{
			return this.SpecialTitleColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the color of the text displayed in a special 
		/// header in it's highlighted state
		/// </summary>
		[Description("The color of the text displayed in a special Expandos titlebar in it's highlighted state")]
		public Color SpecialTitleHotColor
		{
			get
			{
				return this.specialTitleHot;
			}

			set
			{
				if (this.specialTitleHot != value)
				{
					this.specialTitleHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialTitleHotColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialTitleHotColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialTitleHotColor()
		{
			return this.SpecialTitleHotColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the color of the text displayed in a normal 
		/// header in it's normal state
		/// </summary>
		[Description("The color of the text displayed in a normal Expandos titlebar in it's normal state")]
		public Color NormalTitleColor
		{
			get
			{
				return this.normalTitle;
			}

			set
			{
				if (this.normalTitle != value)
				{
					this.normalTitle = value;

					// set the NormalTitleHotColor as well just in case
					// it isn't/wasn't set during UIFILE parsing
					if (this.NormalTitleHotColor == Color.Transparent)
					{
						this.NormalTitleHotColor = value;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalTitleColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalTitleColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalTitleColor()
		{
			return this.NormalTitleColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the color of the text displayed in a normal 
		/// header in it's highlighted state
		/// </summary>
		[Description("The color of the text displayed in a normal Expandos titlebar in it's highlighted state")]
		public Color NormalTitleHotColor
		{
			get
			{
				return this.normalTitleHot;
			}

			set
			{
				if (this.normalTitleHot != value)
				{
					this.normalTitleHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalTitleHotColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalTitleHotColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalTitleHotColor()
		{
			return this.NormalTitleHotColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the alignment of the text displayed in a special header
		/// </summary>
		[DefaultValue(ContentAlignment.MiddleLeft), 
		Description("The alignment of the text displayed in a special Expandos titlebar")]
		public ContentAlignment SpecialAlignment
		{
			get
			{
				return this.specialAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.specialAlignment != value)
				{
					this.specialAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the text displayed in a normal header
		/// </summary>
		[DefaultValue(ContentAlignment.MiddleLeft), 
		Description("The alignment of the text displayed in a normal Expandos titlebar")]
		public ContentAlignment NormalAlignment
		{
			get
			{
				return this.normalAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.normalAlignment != value)
				{
					this.normalAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets whether the header's background should use a gradient fill
		/// </summary>
		[DefaultValue(false),
		Description("")]
		public bool TitleGradient
		{
			get
			{
				return this.useTitleGradient;
			}

			set
			{
				if (this.useTitleGradient != value)
				{
					this.useTitleGradient = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets the start Color of a header's gradient fill for a special 
		/// Expando
		/// </summary>
		[Description("")]
		public Color SpecialGradientStartColor
		{
			get
			{
				return this.specialGradientStartColor;
			}

			set
			{
				if (this.specialGradientStartColor != value)
				{
					this.specialGradientStartColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialGradientStartColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialGradientStartColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialGradientStartColor()
		{
			return this.SpecialGradientStartColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the end Color of a header's gradient fill for a special 
		/// Expando
		/// </summary>
		[Description("")]
		public Color SpecialGradientEndColor
		{
			get
			{
				return this.specialGradientEndColor;
			}

			set
			{
				if (this.specialGradientEndColor != value)
				{
					this.specialGradientEndColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the SpecialGradientEndColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the SpecialGradientEndColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeSpecialGradientEndColor()
		{
			return this.SpecialGradientEndColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the start Color of a header's gradient fill for a normal 
		/// Expando
		/// </summary>
		[Description("")]
		public Color NormalGradientStartColor
		{
			get
			{
				return this.normalGradientStartColor;
			}

			set
			{
				if (this.normalGradientStartColor != value)
				{
					this.normalGradientStartColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalGradientStartColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalGradientStartColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalGradientStartColor()
		{
			return this.NormalGradientStartColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets the end Color of a header's gradient fill for a normal 
		/// Expando
		/// </summary>
		[Description("")]
		public Color NormalGradientEndColor
		{
			get
			{
				return this.normalGradientEndColor;
			}

			set
			{
				if (this.normalGradientEndColor != value)
				{
					this.normalGradientEndColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		/// Specifies whether the NormalGradientEndColor property should be 
		/// serialized at design time
		/// </summary>
		/// <returns>true if the NormalGradientEndColor property should be 
		/// serialized, false otherwise</returns>
		private bool ShouldSerializeNormalGradientEndColor()
		{
			return this.NormalGradientEndColor != Color.Empty;
		}


		/// <summary>
		/// Gets or sets how far along the header the gradient starts
		/// </summary>
		[DefaultValue(0.5f),
		Description("")]
		public float GradientOffset
		{
			get
			{
				return this.gradientOffset;
			}

			set
			{
				if (value < 0)
				{
					value = 0f;
				}
				else if (value > 1)
				{
					value = 1f;
				}
				
				if (this.gradientOffset != value)
				{
					this.gradientOffset = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		/// <summary>
		///Gets or sets the radius of the corners on the header
		/// </summary>
		[DefaultValue(2),
		Description("")]
		public int TitleRadius
		{
			get
			{
				return this.titleRadius;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}
				else if (value > this.BackImageHeight)
				{
					value = this.BackImageHeight;
				}
				
				if (this.titleRadius != value)
				{
					this.titleRadius = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region Expando

		/// <summary>
		/// Gets or sets the Expando the HeaderInfo belongs to
		/// </summary>
		protected internal Expando Expando
		{
			get
			{
				return this.owner;
			}

			set
			{
				this.owner = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal bool RightToLeft
		{
			get
			{
				return this.rightToLeft;
			}

			set
			{
				this.rightToLeft = value;
			}
		}

		#endregion

		#endregion


		#region HeaderInfoSurrogate

		/// <summary>
		/// A class that is serialized instead of a HeaderInfo (as 
		/// HeaderInfos contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class HeaderInfoSurrogate : ISerializable
		{
			#region Class Data
			
			/// <summary>
			/// See Font.Name.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string FontName;
			
			/// <summary>
			/// See Font.Size.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public float FontSize;
			
			/// <summary>
			/// See Font.Style.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public FontStyle FontStyle;
			
			/// <summary>
			/// See HeaderInfo.Margin.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public int Margin;
			
			/// <summary>
			/// See HeaderInfo.SpecialBackImage.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialBackImage;
			
			/// <summary>
			/// See HeaderInfo.NormalBackImage.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalBackImage;
			
			/// <summary>
			/// See HeaderInfo.SpecialTitle.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialTitle;
			
			/// <summary>
			/// See HeaderInfo.NormalTitle.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalTitle;
			
			/// <summary>
			/// See HeaderInfo.SpecialTitleHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialTitleHot;
			
			/// <summary>
			/// See HeaderInfo.NormalTitleHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalTitleHot;
			
			/// <summary>
			/// See HeaderInfo.SpecialAlignment.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public ContentAlignment SpecialAlignment;
			
			/// <summary>
			/// See HeaderInfo.NormalAlignment.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public ContentAlignment NormalAlignment;
			
			/// <summary>
			/// See HeaderInfo.SpecialPadding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding SpecialPadding;
			
			/// <summary>
			/// See HeaderInfo.NormalPadding.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Padding NormalPadding;
			
			/// <summary>
			/// See HeaderInfo.SpecialBorder.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Border SpecialBorder;
			
			/// <summary>
			/// See HeaderInfo.NormalBorder.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public Border NormalBorder;
			
			/// <summary>
			/// See HeaderInfo.SpecialBorderColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialBorderColor;
			
			/// <summary>
			/// See HeaderInfo.NormalBorderColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalBorderColor;
			
			/// <summary>
			/// See HeaderInfo.SpecialBackColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialBackColor;
			
			/// <summary>
			/// See HeaderInfo.NormalBackColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalBackColor;
			
			/// <summary>
			/// See HeaderInfo.SpecialArrowUp.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialArrowUp", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowUp;
			
			/// <summary>
			/// See HeaderInfo.SpecialArrowUpHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialArrowUpHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowUpHot;
			
			/// <summary>
			/// See HeaderInfo.SpecialArrowDown.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialArrowDown", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowDown;
			
			/// <summary>
			/// See HeaderInfo.SpecialArrowDownHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("SpecialArrowDownHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowDownHot;
			
			/// <summary>
			/// See HeaderInfo.NormalArrowUp.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalArrowUp", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowUp;
			
			/// <summary>
			/// See HeaderInfo.NormalArrowUpHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalArrowUpHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowUpHot;
			
			/// <summary>
			/// See HeaderInfo.NormalArrowDown.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalArrowDown", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowDown;
			
			/// <summary>
			/// See HeaderInfo.NormalArrowDownHot.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			[XmlElementAttribute("NormalArrowDownHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowDownHot;
			
			/// <summary>
			/// See HeaderInfo.TitleGradient.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public bool TitleGradient;
			
			/// <summary>
			/// See HeaderInfo.SpecialGradientStartColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialGradientStartColor;
			
			/// <summary>
			/// See HeaderInfo.SpecialGradientEndColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string SpecialGradientEndColor;
			
			/// <summary>
			/// See HeaderInfo.NormalGradientStartColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalGradientStartColor;
			
			/// <summary>
			/// See HeaderInfo.NormalGradientEndColor.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public string NormalGradientEndColor;
			
			/// <summary>
			/// See HeaderInfo.GradientOffset.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public float GradientOffset;
			
			/// <summary>
			/// See HeaderInfo.TitleRadius.  This member is not 
			/// intended to be used directly from your code.
			/// </summary>
			public int TitleRadius;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the HeaderInfoSurrogate class with default settings
			/// </summary>
			public HeaderInfoSurrogate()
			{
				this.FontName = null;
				this.FontSize = 8.25f;
				this.FontStyle = FontStyle.Regular;
				this.Margin = 15;

				this.SpecialBackImage = new byte[0];
				this.NormalBackImage = new byte[0];

				this.SpecialTitle = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalTitle = ThemeManager.ConvertColorToString(Color.Empty);
				this.SpecialTitleHot = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalTitleHot = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialAlignment = ContentAlignment.MiddleLeft;
				this.NormalAlignment = ContentAlignment.MiddleLeft;

				this.SpecialPadding = Padding.Empty;
				this.NormalPadding = Padding.Empty;

				this.SpecialBorder = Border.Empty;
				this.NormalBorder = Border.Empty;
				this.SpecialBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				
				this.SpecialBackColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBackColor = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialArrowUp = new byte[0];
				this.SpecialArrowUpHot = new byte[0];
				this.SpecialArrowDown = new byte[0];
				this.SpecialArrowDownHot = new byte[0];
				this.NormalArrowUp = new byte[0];
				this.NormalArrowUpHot = new byte[0];
				this.NormalArrowDown = new byte[0];
				this.NormalArrowDownHot = new byte[0];

				this.TitleGradient = false;
				this.SpecialGradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.SpecialGradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalGradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalGradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientOffset = 0.5f;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the HeaderInfoSurrogate with data that is to be 
			/// serialized from the specified HeaderInfo
			/// </summary>
			/// <param name="headerInfo">The HeaderInfo that contains the data 
			/// to be serialized</param>
			public void Load(HeaderInfo headerInfo)
			{
				if (headerInfo.TitleFont != null)
				{
					this.FontName = headerInfo.TitleFont.Name;
					this.FontSize = headerInfo.TitleFont.SizeInPoints;
					this.FontStyle = headerInfo.TitleFont.Style;
				}

				this.Margin = headerInfo.Margin;

				this.SpecialBackImage = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialBackImage);
				this.NormalBackImage = ThemeManager.ConvertImageToByteArray(headerInfo.NormalBackImage);

				this.SpecialTitle = ThemeManager.ConvertColorToString(headerInfo.SpecialTitleColor);
				this.NormalTitle = ThemeManager.ConvertColorToString(headerInfo.NormalTitleColor);
				this.SpecialTitleHot = ThemeManager.ConvertColorToString(headerInfo.SpecialTitleHotColor);
				this.NormalTitleHot = ThemeManager.ConvertColorToString(headerInfo.NormalTitleHotColor);

				this.SpecialAlignment = headerInfo.SpecialAlignment;
				this.NormalAlignment = headerInfo.NormalAlignment;

				this.SpecialPadding = headerInfo.SpecialPadding;
				this.NormalPadding = headerInfo.NormalPadding;

				this.SpecialBorder = headerInfo.SpecialBorder;
				this.NormalBorder = headerInfo.NormalBorder;
				this.SpecialBorderColor = ThemeManager.ConvertColorToString(headerInfo.SpecialBorderColor);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(headerInfo.NormalBorderColor);
				
				this.SpecialBackColor = ThemeManager.ConvertColorToString(headerInfo.SpecialBackColor);
				this.NormalBackColor = ThemeManager.ConvertColorToString(headerInfo.NormalBackColor);

				this.SpecialArrowUp = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowUp);
				this.SpecialArrowUpHot = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowUpHot);
				this.SpecialArrowDown = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowDown);
				this.SpecialArrowDownHot = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowDownHot);
				this.NormalArrowUp = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowUp);
				this.NormalArrowUpHot = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowUpHot);
				this.NormalArrowDown = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowDown);
				this.NormalArrowDownHot = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowDownHot);

				this.TitleGradient = headerInfo.TitleGradient;
				this.SpecialGradientStartColor = ThemeManager.ConvertColorToString(headerInfo.SpecialGradientStartColor);
				this.SpecialGradientEndColor = ThemeManager.ConvertColorToString(headerInfo.SpecialGradientEndColor);
				this.NormalGradientStartColor = ThemeManager.ConvertColorToString(headerInfo.NormalGradientStartColor);
				this.NormalGradientEndColor = ThemeManager.ConvertColorToString(headerInfo.NormalGradientEndColor);
				this.GradientOffset = headerInfo.GradientOffset;
			}


			/// <summary>
			/// Returns a HeaderInfo that contains the deserialized HeaderInfoSurrogate data
			/// </summary>
			/// <returns>A HeaderInfo that contains the deserialized HeaderInfoSurrogate data</returns>
			public HeaderInfo Save()
			{
				HeaderInfo headerInfo = new HeaderInfo();

				if (this.FontName != null)
				{
					headerInfo.TitleFont = new Font(this.FontName, this.FontSize, this.FontStyle);
				}

				headerInfo.Margin = this.Margin;

				headerInfo.SpecialBackImage = ThemeManager.ConvertByteArrayToImage(this.SpecialBackImage);
				headerInfo.NormalBackImage = ThemeManager.ConvertByteArrayToImage(this.NormalBackImage);

				headerInfo.SpecialTitleColor = ThemeManager.ConvertStringToColor(this.SpecialTitle);
				headerInfo.NormalTitleColor = ThemeManager.ConvertStringToColor(this.NormalTitle);
				headerInfo.SpecialTitleHotColor = ThemeManager.ConvertStringToColor(this.SpecialTitleHot);
				headerInfo.NormalTitleHotColor = ThemeManager.ConvertStringToColor(this.NormalTitleHot);

				headerInfo.SpecialAlignment = this.SpecialAlignment;
				headerInfo.NormalAlignment = this.NormalAlignment;
				
				headerInfo.SpecialPadding = this.SpecialPadding;
				headerInfo.NormalPadding = this.NormalPadding;

				headerInfo.SpecialBorder = this.SpecialBorder;
				headerInfo.NormalBorder = this.NormalBorder;
				headerInfo.SpecialBorderColor = ThemeManager.ConvertStringToColor(this.SpecialBorderColor);
				headerInfo.NormalBorderColor = ThemeManager.ConvertStringToColor(this.NormalBorderColor);

				headerInfo.SpecialBackColor = ThemeManager.ConvertStringToColor(this.SpecialBackColor);
				headerInfo.NormalBackColor = ThemeManager.ConvertStringToColor(this.NormalBackColor);

				headerInfo.SpecialArrowUp = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowUp);
				headerInfo.SpecialArrowUpHot = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowUpHot);
				headerInfo.SpecialArrowDown = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowDown);
				headerInfo.SpecialArrowDownHot = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowDownHot);
				headerInfo.NormalArrowUp = ThemeManager.ConvertByteArrayToImage(this.NormalArrowUp);
				headerInfo.NormalArrowUpHot = ThemeManager.ConvertByteArrayToImage(this.NormalArrowUpHot);
				headerInfo.NormalArrowDown = ThemeManager.ConvertByteArrayToImage(this.NormalArrowDown);
				headerInfo.NormalArrowDownHot = ThemeManager.ConvertByteArrayToImage(this.NormalArrowDownHot);

				headerInfo.TitleGradient = this.TitleGradient;
				headerInfo.SpecialGradientStartColor = ThemeManager.ConvertStringToColor(this.SpecialGradientStartColor);
				headerInfo.SpecialGradientEndColor = ThemeManager.ConvertStringToColor(this.SpecialGradientEndColor);
				headerInfo.NormalGradientStartColor = ThemeManager.ConvertStringToColor(this.NormalGradientStartColor);
				headerInfo.NormalGradientEndColor = ThemeManager.ConvertStringToColor(this.NormalGradientEndColor);
				headerInfo.GradientOffset = this.GradientOffset;
				
				return headerInfo;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the HeaderInfoSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);

				info.AddValue("FontName", this.FontName);
				info.AddValue("FontSize", this.FontSize);
				info.AddValue("FontStyle", this.FontStyle);

				info.AddValue("Margin", this.Margin);

				info.AddValue("SpecialBackImage", this.SpecialBackImage);
				info.AddValue("NormalBackImage", this.NormalBackImage);

				info.AddValue("SpecialTitle", this.SpecialTitle);
				info.AddValue("NormalTitle", this.NormalTitle);
				info.AddValue("SpecialTitleHot", this.SpecialTitleHot);
				info.AddValue("NormalTitleHot", this.NormalTitleHot);

				info.AddValue("SpecialAlignment", this.SpecialAlignment);
				info.AddValue("NormalAlignment", this.NormalAlignment);

				info.AddValue("SpecialPadding", this.SpecialPadding);
				info.AddValue("NormalPadding", this.NormalPadding);

				info.AddValue("SpecialBorder", this.SpecialBorder);
				info.AddValue("NormalBorder", this.NormalBorder);
				info.AddValue("SpecialBorderColor", this.SpecialBorderColor);
				info.AddValue("NormalBorderColor", this.NormalBorderColor);

				info.AddValue("SpecialBackColor", this.SpecialBackColor);
				info.AddValue("NormalBackColor", this.NormalBackColor);

				info.AddValue("SpecialArrowUp", this.SpecialArrowUp);
				info.AddValue("SpecialArrowUpHot", this.SpecialArrowUpHot);
				info.AddValue("SpecialArrowDown", this.SpecialArrowDown);
				info.AddValue("SpecialArrowDownHot", this.SpecialArrowDownHot);
				info.AddValue("NormalArrowUp", this.NormalArrowUp);
				info.AddValue("NormalArrowUpHot", this.NormalArrowUpHot);
				info.AddValue("NormalArrowDown", this.NormalArrowDown);
				info.AddValue("NormalArrowDownHot", this.NormalArrowDownHot);

				info.AddValue("TitleGradient", this.TitleGradient);
				info.AddValue("SpecialGradientStartColor", this.SpecialGradientStartColor);
				info.AddValue("SpecialGradientEndColor", this.SpecialGradientEndColor);
				info.AddValue("NormalGradientStartColor", this.NormalGradientStartColor);
				info.AddValue("NormalGradientEndColor", this.NormalGradientEndColor);
				info.AddValue("GradientOffset", this.GradientOffset);
			}


			/// <summary>
			/// Initializes a new instance of the HeaderInfoSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the HeaderInfoSurrogate</param>
			/// <param name="context">The source from which the HeaderInfoSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected HeaderInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");
				
				this.FontName = info.GetString("FontName");
				this.FontSize = info.GetSingle("FontSize");
				this.FontStyle = (FontStyle) info.GetValue("FontStyle", typeof(FontStyle));

				this.Margin = info.GetInt32("Margin");
				
				this.SpecialBackImage = (byte[]) info.GetValue("SpecialBackImage", typeof(byte[]));
				this.NormalBackImage = (byte[]) info.GetValue("NormalBackImage", typeof(byte[]));
				
				this.SpecialTitle = info.GetString("SpecialTitle");
				this.NormalTitle = info.GetString("NormalTitle");
				this.SpecialTitleHot = info.GetString("SpecialTitleHot");
				this.NormalTitleHot = info.GetString("NormalTitleHot");
				
				this.SpecialAlignment = (ContentAlignment) info.GetValue("SpecialAlignment", typeof(ContentAlignment));
				this.NormalAlignment = (ContentAlignment) info.GetValue("NormalAlignment", typeof(ContentAlignment));

				this.SpecialPadding = (Padding) info.GetValue("SpecialPadding", typeof(Padding));
				this.NormalPadding = (Padding) info.GetValue("NormalPadding", typeof(Padding));
				
				this.SpecialBorder = (Border) info.GetValue("SpecialBorder", typeof(Border));
				this.NormalBorder = (Border) info.GetValue("NormalBorder", typeof(Border));
				this.SpecialBorderColor = info.GetString("SpecialBorderColor");
				this.NormalBorderColor = info.GetString("NormalBorderColor");
				
				this.SpecialBackColor = info.GetString("SpecialBackColor");
				this.NormalBackColor = info.GetString("NormalBackColor");
				
				this.SpecialArrowUp = (byte[]) info.GetValue("SpecialArrowUp", typeof(byte[]));
				this.SpecialArrowUpHot = (byte[]) info.GetValue("SpecialArrowUpHot", typeof(byte[]));
				this.SpecialArrowDown = (byte[]) info.GetValue("SpecialArrowDown", typeof(byte[]));
				this.SpecialArrowDownHot = (byte[]) info.GetValue("SpecialArrowDownHot", typeof(byte[]));
				this.NormalArrowUp = (byte[]) info.GetValue("NormalArrowUp", typeof(byte[]));
				this.NormalArrowUpHot = (byte[]) info.GetValue("NormalArrowUpHot", typeof(byte[]));
				this.NormalArrowDown = (byte[]) info.GetValue("NormalArrowDown", typeof(byte[]));
				this.NormalArrowDownHot = (byte[]) info.GetValue("NormalArrowDownHot", typeof(byte[]));
				
				this.TitleGradient = info.GetBoolean("TitleGradient");
				this.SpecialGradientStartColor = info.GetString("SpecialGradientStartColor");
				this.SpecialGradientEndColor = info.GetString("SpecialGradientEndColor");
				this.NormalGradientStartColor = info.GetString("NormalGradientStartColor");
				this.NormalGradientEndColor = info.GetString("NormalGradientEndColor");
				this.GradientOffset = info.GetSingle("GradientOffset");
			}

			#endregion
		}

		#endregion
	}


	#region HeaderInfoConverter

	/// <summary>
	/// A custom TypeConverter used to help convert HeaderInfos from 
	/// one Type to another
	/// </summary>
	internal class HeaderInfoConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is HeaderInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			// set the order in which the properties appear 
			// in the property window
			
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(HeaderInfo), attributes);

			string[] s = new string[33];
			s[0] = "TitleFont";
			s[1] = "TitleGradient";
			s[2] = "NormalGradientStartColor";
			s[3] = "NormalGradientEndColor";
			s[4] = "SpecialGradientStartColor";
			s[5] = "SpecialGradientEndColor";
			s[6] = "GradientOffset";
			s[7] = "TitleRadius";
			s[8] = "NormalBackImage";
			s[9] = "SpecialBackImage";
			s[10] = "NormalArrowUp";
			s[11] = "NormalArrowUpHot";
			s[12] = "NormalArrowDown";
			s[13] = "NormalArrowDownHot";
			s[14] = "SpecialArrowUp";
			s[15] = "SpecialArrowUpHot";
			s[16] = "SpecialArrowDown";
			s[17] = "SpecialArrowDownHot";
			s[18] = "NormalAlignment";
			s[19] = "SpecialAlignment";
			s[20] = "NormalBackColor";
			s[21] = "SpecialBackColor";
			s[22] = "NormalBorder";
			s[23] = "SpecialBorder";
			s[24] = "NormalBorderColor";
			s[25] = "SpecialBorderColor";
			s[26] = "NormalPadding";
			s[27] = "SpecialPadding";
			s[28] = "NormalTitleColor";
			s[29] = "NormalTitleHotColor";
			s[30] = "SpecialTitleColor";
			s[31] = "SpecialTitleHotColor";
			s[32] = "Margin";

			return collection.Sort(s);
		}
	}

	#endregion

	#endregion


	#region Border Class

	/// <summary>
	/// Specifies the width of the border along each edge of an object
	/// </summary>
	[Serializable(),  
	TypeConverter(typeof(BorderConverter))]
	public class Border
	{
		#region Class Data
		
		/// <summary>
		/// Represents a Border structure with its properties 
		/// left uninitialized
		/// </summary>
		[NonSerialized()]
		public static readonly Border Empty = new Border(0, 0, 0, 0);
		
		/// <summary>
		/// The width of the left border
		/// </summary>
		private int left;
		
		/// <summary>
		/// The width of the right border
		/// </summary>
		private int right;
		
		/// <summary>
		/// The width of the top border
		/// </summary>
		private int top;
		
		/// <summary>
		/// The width of the bottom border
		/// </summary>
		private int bottom;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Border class with default settings
		/// </summary>
		public Border() : this(0, 0, 0, 0)
		{

		}


		/// <summary>
		/// Initializes a new instance of the Border class
		/// </summary>
		/// <param name="left">The width of the left border</param>
		/// <param name="top">The Height of the top border</param>
		/// <param name="right">The width of the right border</param>
		/// <param name="bottom">The Height of the bottom border</param>
		public Border(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Tests whether obj is a Border structure with the same values as 
		/// this Border structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a Border structure 
		/// and its Left, Top, Right, and Bottom properties are equal to 
		/// the corresponding properties of this Border structure; 
		/// otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Border))
			{
				return false;
			}

			Border border = (Border) obj;

			if (((border.Left == this.Left) && (border.Top == this.Top)) && (border.Right == this.Right))
			{
				return (border.Bottom == this.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Returns the hash code for this Border structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// border</returns>
		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the value of the left border
		/// </summary>
		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		/// <summary>
		/// Gets or sets the value of the right border
		/// </summary>
		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		/// <summary>
		/// Gets or sets the value of the top border
		/// </summary>
		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}


		/// <summary>
		/// Gets or sets the value of the bottom border
		/// </summary>
		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


		/// <summary>
		/// Tests whether all numeric properties of this Border have 
		/// values of zero
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

		#endregion


		#region Operators

		/// <summary>
		/// Tests whether two Border structures have equal Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Border structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Border structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two Border structures 
		/// have equal Left, Top, Right, and Bottom properties</returns>
		public static bool operator ==(Border left, Border right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Tests whether two Border structures differ in their Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Border structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Border structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Left, Top, Right, 
		/// and Bottom properties of the two Border structures are unequal; 
		/// otherwise false</returns>
		public static bool operator !=(Border left, Border right)
		{
			return !(left == right);
		}

		#endregion
	}


	#region BorderConverter

	/// <summary>
	/// A custom TypeConverter used to help convert Borders from 
	/// one Type to another
	/// </summary>
	internal class BorderConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="sourceType">A Type that represents the type you 
		/// want to convert from</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		/// <summary>
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="destinationType">A Type that represents the type you 
		/// want to convert to</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		/// <summary>
		/// Converts the given object to the type of this converter, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="culture">The CultureInfo to use as the current culture</param>
		/// <param name="value">The Object to convert</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Border(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Border))
			{
				Border b = (Border) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, b.Left);
				s[1] = converter.ConvertToString(context, culture, b.Top);
				s[2] = converter.ConvertToString(context, culture, b.Right);
				s[3] = converter.ConvertToString(context, culture, b.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Border))
			{
				Border b = (Border) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Border).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = b.Left;
					o[1] = b.Top;
					o[2] = b.Right;
					o[3] = b.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Creates an instance of the Type that this TypeConverter is associated 
		/// with, using the specified context, given a set of property values for 
		/// the object
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="propertyValues">An IDictionary of new property values</param>
		/// <returns>An Object representing the given IDictionary, or a null 
		/// reference if the object cannot be created</returns>
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Border((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		/// <summary>
		/// Returns whether changing a value on this object requires a call to 
		/// CreateInstance to create a new value, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <returns>true if changing a property on this object requires a call 
		/// to CreateInstance to create a new value; otherwise, false</returns>
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Border), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		/// <summary>
		/// Returns whether this object supports properties, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <returns>true if GetProperties should be called to find the properties of this 
		/// object; otherwise, false</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	#endregion

	#endregion


	#region Padding Class

	/// <summary>
	/// Specifies the amount of space between the border and any contained 
	/// items along each edge of an object
	/// </summary>
	[Serializable, 
	TypeConverter(typeof(PaddingConverter))]
	public class Padding
	{
		#region Class Data
		
		/// <summary>
		/// Represents a Padding structure with its properties 
		/// left uninitialized
		/// </summary>
		[NonSerialized()]
		public static readonly Padding Empty = new Padding(0, 0, 0, 0);
		
		/// <summary>
		/// The width of the left padding
		/// </summary>
		private int left;
		
		/// <summary>
		/// The width of the right padding
		/// </summary>
		private int right;
		
		/// <summary>
		/// The width of the top padding
		/// </summary>
		private int top;
		
		/// <summary>
		/// The width of the bottom padding
		/// </summary>
		private int bottom;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Padding class with default settings
		/// </summary>
		public Padding() : this(0, 0, 0, 0)
		{

		}


		/// <summary>
		/// Initializes a new instance of the Padding class
		/// </summary>
		/// <param name="left">The width of the left padding value</param>
		/// <param name="top">The height of top padding value</param>
		/// <param name="right">The width of the right padding value</param>
		/// <param name="bottom">The height of bottom padding value</param>
		public Padding(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Tests whether obj is a Padding structure with the same values as 
		/// this Padding structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a Padding structure 
		/// and its Left, Top, Right, and Bottom properties are equal to 
		/// the corresponding properties of this Padding structure; 
		/// otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Padding))
			{
				return false;
			}

			Padding padding = (Padding) obj;

			if (((padding.Left == this.Left) && (padding.Top == this.Top)) && (padding.Right == this.Right))
			{
				return (padding.Bottom == this.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Returns the hash code for this Padding structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// padding</returns>
		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the width of the left padding value
		/// </summary>
		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		/// <summary>
		/// Gets or sets the width of the right padding value
		/// </summary>
		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of the top padding value
		/// </summary>
		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of the bottom padding value
		/// </summary>
		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


		/// <summary>
		/// Tests whether all numeric properties of this Padding have 
		/// values of zero
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

		#endregion


		#region Operators

		/// <summary>
		/// Tests whether two Padding structures have equal Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Padding structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Padding structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two Padding structures 
		/// have equal Left, Top, Right, and Bottom properties</returns>
		public static bool operator ==(Padding left, Padding right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Tests whether two Padding structures differ in their Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Padding structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Padding structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Left, Top, Right, 
		/// and Bottom properties of the two Padding structures are unequal; 
		/// otherwise false</returns>
		public static bool operator !=(Padding left, Padding right)
		{
			return !(left == right);
		}

		#endregion
	}


	#region PaddingConverter

	/// <summary>
	/// A custom TypeConverter used to help convert Padding objects from 
	/// one Type to another
	/// </summary>
	internal class PaddingConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the 
		/// given type to the type of this converter, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="sourceType">A Type that represents the type you 
		/// want to convert from</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		/// <summary>
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="destinationType">A Type that represents the type you 
		/// want to convert to</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		/// <summary>
		/// Converts the given object to the type of this converter, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="culture">The CultureInfo to use as the current culture</param>
		/// <param name="value">The Object to convert</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Padding(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Padding))
			{
				Padding p = (Padding) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, p.Left);
				s[1] = converter.ConvertToString(context, culture, p.Top);
				s[2] = converter.ConvertToString(context, culture, p.Right);
				s[3] = converter.ConvertToString(context, culture, p.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Padding))
			{
				Padding p = (Padding) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Padding).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = p.Left;
					o[1] = p.Top;
					o[2] = p.Right;
					o[3] = p.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Creates an instance of the Type that this TypeConverter is associated 
		/// with, using the specified context, given a set of property values for 
		/// the object
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="propertyValues">An IDictionary of new property values</param>
		/// <returns>An Object representing the given IDictionary, or a null 
		/// reference if the object cannot be created</returns>
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Padding((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		/// <summary>
		/// Returns whether changing a value on this object requires a call to 
		/// CreateInstance to create a new value, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <returns>true if changing a property on this object requires a call 
		/// to CreateInstance to create a new value; otherwise, false</returns>
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Padding), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		/// <summary>
		/// Returns whether this object supports properties, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <returns>true if GetProperties should be called to find the properties of this 
		/// object; otherwise, false</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	#endregion

	#endregion


	#region Margin Class

	/// <summary>
	/// Specifies the amount of space arouund an object along each side
	/// </summary>
	[Serializable,  
	TypeConverter(typeof(MarginConverter))]
	public class Margin
	{
		#region Class Data
		
		/// <summary>
		/// Represents a Margin structure with its properties 
		/// left uninitialized
		/// </summary>
		[NonSerialized()]
		public static readonly Margin Empty = new Margin(0, 0, 0, 0);
		
		/// <summary>
		/// The width of the left margin
		/// </summary>
		private int left;
		
		/// <summary>
		/// The width of the right margin
		/// </summary>
		private int right;
		
		/// <summary>
		/// The width of the top margin
		/// </summary>
		private int top;
		
		/// <summary>
		/// The width of the bottom margin
		/// </summary>
		private int bottom;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Margin class with default settings
		/// </summary>
		public Margin() : this(0, 0, 0, 0)
		{

		}


		/// <summary>
		/// Initializes a new instance of the Margin class
		/// </summary>
		/// <param name="left">The width of the left margin value</param>
		/// <param name="top">The height of the top margin value</param>
		/// <param name="right">The width of the right margin value</param>
		/// <param name="bottom">The height of the bottom margin value</param>
		public Margin(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Tests whether obj is a Margin structure with the same values as 
		/// this Border structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a Margin structure 
		/// and its Left, Top, Right, and Bottom properties are equal to 
		/// the corresponding properties of this Margin structure; 
		/// otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Margin))
			{
				return false;
			}

			Margin margin = (Margin) obj;

			if (((margin.Left == this.Left) && (margin.Top == this.Top)) && (margin.Right == this.Right))
			{
				return (margin.Bottom == this.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Returns the hash code for this Margin structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// margin</returns>
		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the left margin value
		/// </summary>
		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		/// <summary>
		/// Gets or sets the right margin value
		/// </summary>
		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		/// <summary>
		/// Gets or sets the top margin value
		/// </summary>
		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}


		/// <summary>
		/// Gets or sets the bottom margin value
		/// </summary>
		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


		/// <summary>
		/// Tests whether all numeric properties of this Margin have 
		/// values of zero
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

		#endregion


		#region Operators

		/// <summary>
		/// Tests whether two Margin structures have equal Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Margin structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Margin structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two Margin structures 
		/// have equal Left, Top, Right, and Bottom properties</returns>
		public static bool operator ==(Margin left, Margin right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Tests whether two Margin structures differ in their Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The Margin structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The Margin structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Left, Top, Right, 
		/// and Bottom properties of the two Margin structures are unequal; 
		/// otherwise false</returns>
		public static bool operator !=(Margin left, Margin right)
		{
			return !(left == right);
		}

		#endregion
	}


	#region MarginConverter

	/// <summary>
	/// A custom TypeConverter used to help convert Margins from 
	/// one Type to another
	/// </summary>
	internal class MarginConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the 
		/// given type to the type of this converter, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="sourceType">A Type that represents the type you 
		/// want to convert from</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		/// <summary>
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="destinationType">A Type that represents the type you 
		/// want to convert to</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		/// <summary>
		/// Converts the given object to the type of this converter, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="culture">The CultureInfo to use as the current culture</param>
		/// <param name="value">The Object to convert</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Margin(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Margin))
			{
				Margin m = (Margin) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, m.Left);
				s[1] = converter.ConvertToString(context, culture, m.Top);
				s[2] = converter.ConvertToString(context, culture, m.Right);
				s[3] = converter.ConvertToString(context, culture, m.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Margin))
			{
				Margin m = (Margin) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Margin).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = m.Left;
					o[1] = m.Top;
					o[2] = m.Right;
					o[3] = m.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Creates an instance of the Type that this TypeConverter is associated 
		/// with, using the specified context, given a set of property values for 
		/// the object
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="propertyValues">An IDictionary of new property values</param>
		/// <returns>An Object representing the given IDictionary, or a null 
		/// reference if the object cannot be created</returns>
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Margin((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		/// <summary>
		/// Returns whether changing a value on this object requires a call to 
		/// CreateInstance to create a new value, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <returns>true if changing a property on this object requires a call 
		/// to CreateInstance to create a new value; otherwise, false</returns>
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Margin), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		/// <summary>
		/// Returns whether this object supports properties, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <returns>true if GetProperties should be called to find the properties of this 
		/// object; otherwise, false</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	#endregion

	#endregion Margin Class


	#region ImageStretchMode

	/// <summary>
	/// Specifies how images should fill objects
	/// </summary>
	public enum ImageStretchMode
	{
		/// <summary>
		/// Use default settings
		/// </summary>
		Normal = 0,
		
		/// <summary>
		/// The image is transparent
		/// </summary>
		Transparent = 2,
		
		/// <summary>
		/// The image should be tiled
		/// </summary>
		Tile = 3,
		
		/// <summary>
		/// The image should be stretched to fit the objects width 
		/// </summary>
		Horizontal = 5,
		
		/// <summary>
		/// The image should be stretched to fill the object
		/// </summary>
		Stretch = 6,
		
		/// <summary>
		/// The image is stored in ARGB format
		/// </summary>
		ARGBImage = 7
	}

	#endregion
}
