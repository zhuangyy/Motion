// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//

namespace Motion.Controls
{
	using System;
	using System.Drawing;

	/// <summary>
	/// IMotionDetector interface
	/// </summary>
	public interface IMotionDetector
	{
		/// <summary>
		/// Motion level calculation - calculate or not motion level
		/// </summary>
		bool MotionLevelCalculation{ set; get; }

		/// <summary>
		/// Motion level - amount of changes in percents
		/// </summary>
		double MotionLevel{ get; }

		/// <summary>
		/// Motion level - amount of changes in percents
		/// </summary>
		double AlarmLevel { get; set; }

		/// <summary>
		/// Highlight Motion Region
		/// </summary>
		bool Highlight { get; set; }

		/// <summary>
		/// Process new frame
		/// </summary>
		bool ProcessFrame( ref Bitmap image );

		/// <summary>
		/// Reset detector to initial state
		/// </summary>
		void Reset( );
	}
}
