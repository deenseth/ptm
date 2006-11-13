using System;
using System.Collections;

namespace PTM.Framework.Infos
{
	/// <summary>
	/// ApplicationLog contains the information associatied with each running application.
	/// </summary>
	public class ApplicationLog
	{
		public ApplicationLog()
		{
		}//ApplicationLog

		/// <summary>
		/// Application Active Time 
		/// </summary>

		public int ActiveTime
		{
			get { return activeTime; }
			set { activeTime = value; }
		}//ActiveTime

		/// <summary>
		/// Application Caption
		/// </summary>
		public string Caption
		{
			get { return caption; }
			set { caption = value; }
		}//Caption

		/// <summary>
		/// Application Name
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}//Name

		/// <summary>
		/// Application Id
		/// </summary>
		public int Id
		{
			get { return id; }
			set { id = value; }
		}//Id

		/// <summary>
		/// Application Full Path
		/// </summary>
		public string ApplicationFullPath
		{
			get { return applicationFullPath; }
			set { applicationFullPath = value; }
		}//ApplicationFullPath

		int activeTime;
		string caption;
		string name;
		int id;
		string applicationFullPath;

		/// <summary>
		/// Application Task log Id
		/// </summary>
		public int TaskLogId
		{
			get { return taskLogId; }
			set { taskLogId = value; }
		}//TaskLogId

		/// <summary>
		/// ProcessId of the Application
		/// </summary>
		public int ProcessId
		{
			get { return processId; }
			set {processId = value; }
		}//ProcessId

		/// <summary>
		/// Last time this Application Log was updated
		/// </summary>
		public DateTime LastUpdateTime
		{
			get { return lastUpdateTime; }
			set { lastUpdateTime= value; }
		}//LastUpdateTime

		int taskLogId;
		int processId;
		DateTime lastUpdateTime;
	
	}//end of class ApplicationLog
}//end of namespace PTM.Framework.Infos