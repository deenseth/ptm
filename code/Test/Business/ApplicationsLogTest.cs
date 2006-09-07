using System.Collections;
using System.Threading;
using NUnit.Framework;
using PTM.Business;
using PTM.Data;

namespace PTM.Test.Business
{
	/// <summary>
	/// Summary description for ApplicationsLogTest.
	/// </summary>
	[TestFixture]
	public class ApplicationsLogTest
	{
		public ApplicationsLogTest()
		{
		}

		[SetUp]
		public void SetUp()
		{
			DbHelper.Initialize("test");
			DbHelper.DeleteDataSource();
			PTMDataset ds = new PTMDataset();
			MainModule.Initialize(ds, "test");
		}

		[Test]
		public void Initialize()
		{
		}
		
		[Test]
		public void GetApplicationsLogTest()
		{
			Logs.AddLog(Tasks.RootTasksRow.Id);
			Thread.Sleep(6);
			ArrayList apps;
			apps = ApplicationsLog.GetApplicationsLog(Tasks.RootTasksRow.Id);
			Assert.IsNotNull(apps);
		}

		[TearDown]
		public void TearDown()
		{
			Logs.StopLogging();
			DbHelper.DeleteDataSource();
		}

	}
}
