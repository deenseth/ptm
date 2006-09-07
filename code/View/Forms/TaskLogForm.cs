using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using PTM.Business;
using PTM.Data;

namespace PTM.View.Forms
{
	/// <summary>
	/// Summary description for TaskNotificationForm.
	/// </summary>
	public class TaskLogForm : Form
	{
		private Label label1;
		private ComboBox taskComboBox;
		private Button okButton;
		private Button cancelButton;
		private CheckBox hideDefaultTasksCheckBox;
		private PTM.View.Controls.TasksTreeViewControl tasksTree;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button newButton;
		private System.Windows.Forms.GroupBox groupBox1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public TaskLogForm()
		{
			InitializeComponent();		
			
			tasksTree.Initialize(false);

			this.tasksTree.SelectedTaskChanged+=new EventHandler(taskTree_SelectedTaskChanged);
			if(Tasks.CurrentTaskRow != null)
			{
				tasksTree.SelectedTaskId = Tasks.CurrentTaskRow.ParentId;
			}
			else
			{
				tasksTree.SelectedTaskId = Tasks.RootTasksRow.Id;
			}
			//taskTree_SelectedTaskChanged(null, null);
		}

		public TaskLogForm(int editTaskId)
		{
			InitializeComponent();
			tasksTree.Initialize(false);
			this.tasksTree.SelectedTaskChanged+=new EventHandler(taskTree_SelectedTaskChanged);

			PTMDataset.TasksRow row;
			row = Tasks.FindById(editTaskId);
			
			this.tasksTree.SelectedTaskId = row.ParentId;
			
			//taskTree_SelectedTaskChanged(null, null);
			SetChildTask(row);
		}

		private void TaskLogForm_Load(object sender, EventArgs e)
		{
			this.taskComboBox.Focus();		
		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TaskLogForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.taskComboBox = new System.Windows.Forms.ComboBox();
			this.hideDefaultTasksCheckBox = new System.Windows.Forms.CheckBox();
			this.tasksTree = new PTM.View.Controls.TasksTreeViewControl();
			this.editButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.newButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(256, 252);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Ok";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(344, 252);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Description:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// taskComboBox
			// 
			this.taskComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.taskComboBox.Enabled = false;
			this.taskComboBox.Location = new System.Drawing.Point(72, 8);
			this.taskComboBox.MaxLength = 80;
			this.taskComboBox.Name = "taskComboBox";
			this.taskComboBox.Size = new System.Drawing.Size(264, 21);
			this.taskComboBox.TabIndex = 0;
			// 
			// hideDefaultTasksCheckBox
			// 
			this.hideDefaultTasksCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hideDefaultTasksCheckBox.Checked = true;
			this.hideDefaultTasksCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.hideDefaultTasksCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.hideDefaultTasksCheckBox.Location = new System.Drawing.Point(342, 6);
			this.hideDefaultTasksCheckBox.Name = "hideDefaultTasksCheckBox";
			this.hideDefaultTasksCheckBox.Size = new System.Drawing.Size(88, 32);
			this.hideDefaultTasksCheckBox.TabIndex = 1;
			this.hideDefaultTasksCheckBox.Text = "Hide defaults";
			this.hideDefaultTasksCheckBox.CheckedChanged += new System.EventHandler(this.hideDefaultTasksCheckBox_CheckedChanged);
			// 
			// tasksTree
			// 
			this.tasksTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tasksTree.Location = new System.Drawing.Point(6, 18);
			this.tasksTree.Name = "tasksTree";
			this.tasksTree.SelectedTaskId = -1;
			this.tasksTree.Size = new System.Drawing.Size(318, 192);
			this.tasksTree.TabIndex = 0;
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.editButton.Location = new System.Drawing.Point(344, 120);
			this.editButton.Name = "editButton";
			this.editButton.TabIndex = 4;
			this.editButton.Text = "Edit";
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteButton.Location = new System.Drawing.Point(344, 160);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.TabIndex = 5;
			this.deleteButton.Text = "Delete";
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// newButton
			// 
			this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.newButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.newButton.Location = new System.Drawing.Point(344, 80);
			this.newButton.Name = "newButton";
			this.newButton.TabIndex = 3;
			this.newButton.Text = "New";
			this.newButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.tasksTree);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(6, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(330, 216);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Create in";
			// 
			// TaskLogForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(432, 289);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.newButton);
			this.Controls.Add(this.hideDefaultTasksCheckBox);
			this.Controls.Add(this.taskComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(440, 318);
			this.Name = "TaskLogForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Log";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.TaskLogForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion


		private PTMDataset.TasksRow selectedTaskRow = null;
		private PTMDataset.TasksRow selectedParentTaskRow = null;

		public PTMDataset.TasksRow SelectedTaskRow
		{
			get { return selectedTaskRow; }
		}

		private PTMDataset.TasksDataTable childTasksTable = null;

		private void FillChildTasks(bool showDefaulTasks)
		{
			childTasksTable = new PTMDataset.TasksDataTable();
			PTMDataset.TasksRow[] childRows;
			childRows = Tasks.GetChildTasks(selectedParentTaskRow.Id);

			if(showDefaulTasks)
			{
				foreach (PTMDataset.TasksRow childRow in childRows)
				{
					PTMDataset.TasksRow row;
					row = childTasksTable.NewTasksRow();
					row.ItemArray = childRow.ItemArray;
					childTasksTable.AddTasksRow(row);
				}
				foreach (DefaultTask defaultRow in DefaultTasks.List)
				{
					bool exist = false;
					foreach (PTMDataset.TasksRow childRow in childTasksTable.Rows)
					{
						if(childRow.IsDefaultTask && childRow.DefaultTaskId == defaultRow.DefaultTaskId)
						{
							exist = true;
							break;
						}
					}
					if(!exist)
					{
						PTMDataset.TasksRow row;
						row = childTasksTable.NewTasksRow();
						row.IsDefaultTask = true;
						row.DefaultTaskId = defaultRow.DefaultTaskId;
						row.Description = defaultRow.Description;
						row.ParentId = this.selectedParentTaskRow.Id;
						row.Id = -defaultRow.DefaultTaskId;
						childTasksTable.AddTasksRow(row);
					}
				}
			}
			else
			{
				foreach (PTMDataset.TasksRow childRow in childRows)
				{
					bool exist = false;
					foreach (DefaultTask defaultRow in DefaultTasks.List)
					{
						if(childRow.IsDefaultTask && childRow.DefaultTaskId == defaultRow.DefaultTaskId)
						{
							exist = true;
							break;
						}
					}
					if(!exist)
					{
						PTMDataset.TasksRow row;
						row = childTasksTable.NewTasksRow();
						row.ItemArray = childRow.ItemArray;
						childTasksTable.AddTasksRow(row);
					}
				}
			}
			this.taskComboBox.DisplayMember = childTasksTable.DescriptionColumn.ColumnName;
			this.taskComboBox.ValueMember = childTasksTable.IdColumn.ColumnName;
			this.taskComboBox.DataSource = childTasksTable.DefaultView;
			this.taskComboBox.Enabled = true;
			this.taskComboBox.Focus();
		}
		private void SetChildTask(PTMDataset.TasksRow childTaskRow)
		{
			if(childTasksTable.FindById(childTaskRow.Id)==null)
			{
				PTMDataset.TasksRow row = this.childTasksTable.NewTasksRow();
				row.ItemArray = childTaskRow.ItemArray;
				this.childTasksTable.Rows.InsertAt(row, 0 );
			}
			this.taskComboBox.SelectedValue= childTaskRow.Id;
		}
		
	
		private void hideDefaultTasksCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			FillChildTasks(!this.hideDefaultTasksCheckBox.Checked);
		}


		private bool cancelClose = false;
		private void okButton_Click(object sender, EventArgs e)
		{
			try
			{
				string description = this.taskComboBox.Text.Trim();

				PTMDataset.TasksRow row;
				row = Tasks.FindByParentIdAndDescription(this.tasksTree.SelectedTaskId, description);

				if (row==null)
				{
					this.selectedTaskRow = Tasks.NewTasksRow();
					this.selectedTaskRow.Description = description;
					this.selectedTaskRow.ParentId = this.tasksTree.SelectedTaskId;
					this.selectedTaskRow.Id = Tasks.AddTasksRow(this.selectedTaskRow);
				}
				else
				{
					selectedTaskRow = row;
				}
			}
			catch(ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cancelClose = true;
				return;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing (e);
			if(cancelClose)
			{e.Cancel = true;
			cancelClose= false;}
		}

		private void taskTree_SelectedTaskChanged(object sender, EventArgs e)
		{
			if(this.tasksTree.SelectedTaskId == -1)
				return;

			if(tasksTree.SelectedTaskId == Tasks.RootTasksRow.Id)
			{
				this.editButton.Enabled = false;
				this.deleteButton.Enabled = false;
			}
			else
			{
				this.editButton.Enabled = true;
				this.deleteButton.Enabled = true;
			}

			this.selectedParentTaskRow = Tasks.FindById(tasksTree.SelectedTaskId);

			FillChildTasks(!this.hideDefaultTasksCheckBox.Checked);
		}

		private void newButton_Click(object sender, System.EventArgs e)
		{
			tasksTree.AddNewTask();
		}

		private void editButton_Click(object sender, System.EventArgs e)
		{
			tasksTree.EditSelectedTaskDescription();
		}

		private void deleteButton_Click(object sender, System.EventArgs e)
		{
			tasksTree.DeleteSelectedTask();
		}

	}
}