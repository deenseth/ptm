using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PTM.Framework;
using PTM.Framework.Infos;
using PTM.View.Controls.TreeListViewComponents;
using PTM.View.Forms;

namespace PTM.View.Controls
{
	public class TasksTreeViewControl : UserControl
	{
		private IContainer components;
        public event EventHandler SelectedTaskChanged;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new PTM.View.Controls.TreeListViewComponents.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            this.treeMenu = new System.Windows.Forms.ContextMenu();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.treeView = new PTM.View.Controls.TreeListViewComponents.TreeListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // treeMenu
            // 
            this.treeMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuProperties,
            this.menuItem5,
            this.mnuDelete,
            this.mnuRename});
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 0;
            this.mnuProperties.Text = "Properties...";
            this.mnuProperties.Click += new System.EventHandler(this.mnuProperties_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "-";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 2;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 3;
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.mnuRename_Click);
            // 
            // treeView
            // 
            this.treeView.AllowColumnReorder = true;
            this.treeView.AllowDrop = true;
            this.treeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeView.Comparer = treeListViewItemCollectionComparer1;
            this.treeView.ContextMenu = this.treeMenu;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.MultiSelect = false;
            this.treeView.Name = "treeView";
            this.treeView.ShowGroups = false;
            this.treeView.Size = new System.Drawing.Size(243, 215);
            this.treeView.TabIndex = 0;
            this.treeView.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 200;
            // 
            // TasksTreeViewControl
            // 
            this.Controls.Add(this.treeView);
            this.Name = "TasksTreeViewControl";
            this.Size = new System.Drawing.Size(243, 215);
            this.ResumeLayout(false);

		}

		#endregion

        private TreeListView treeView;
		private int currentSelectedTask = -1;
		private MenuItem menuItem5;
		private ContextMenu treeMenu;
		private MenuItem mnuDelete;
		private MenuItem mnuRename;
		private MenuItem mnuProperties;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        public const string NEW_TASK = "New Task";

        #region Initialization
        public TasksTreeViewControl()
        {
            InitializeComponent();
            InitCommonControls();
            this.treeView.ItemDrag += new ItemDragEventHandler(treeView_ItemDrag);
            this.treeView.DragDrop += new DragEventHandler(treeView_DragDrop);
            this.treeView.DragOver += new DragEventHandler(treeView_DragOver);
            this.treeView.DragEnter += new DragEventHandler(treeView_DragEnter);
            this.treeView.DragLeave += new EventHandler(treeView_DragLeave);
            this.treeView.GiveFeedback += new GiveFeedbackEventHandler(treeView_GiveFeedback);
            this.treeView.DoubleClick += new EventHandler(treeView_DoubleClick);
            this.timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 200;
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		    treeView.SmallImageList = IconsManager.IconsList;
            treeView.SelectedIndexChanged += new EventHandler(treeView_SelectedIndexChanged);
			treeView.AfterLabelEdit += new TreeListViewLabelEditEventHandler(treeView_AfterLabelEdit);
		}

        protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			Tasks.TaskChanged -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TaskDeleting -= new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
		}

		internal void Initialize()
		{
			LoadTree();
			Tasks.TaskChanged += new Tasks.TaskChangeEventHandler(Tasks_TasksRowChanged);
			Tasks.TaskDeleting += new Tasks.TaskChangeEventHandler(Tasks_TasksRowDeleting);
		}

        private void LoadTree()
        {
            treeView.Items.Clear();
            TreeListViewItem nodeParent = CreateNode(Tasks.RootTask);
            this.treeView.Items.Add(nodeParent);
            AddChildNodes(Tasks.RootTask, nodeParent);
        }

        #endregion


        internal void AddNewTask()
		{
			int newId;
			try
			{
                if(treeView.SelectedItems.Count<=0)
                {
                    MessageBox.Show("You need to select a task first.", this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
			    int parentId = (int) treeView.SelectedItems[0].Tag;
			    string newTaskName = GetNewTaskName(parentId);
                newId = Tasks.AddTask(newTaskName, parentId).Id;
			}
			catch (ApplicationException aex)
			{
				MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
			}
			Application.DoEvents();//first insert the new node (event fired)
			treeView.LabelEdit = true;
			TreeListViewItem node = FindTaskNode(newId);
			node.EnsureVisible();
		    node.Selected = true;
			node.BeginEdit();
		}

        private static string GetNewTaskName(int parentId)
        {
            if(Tasks.FindByParentIdAndDescription(parentId, NEW_TASK)==null)
               return NEW_TASK;
            
            int counter = 1;
            string newTaskName;
            do
            {
                newTaskName = NEW_TASK + counter;
                counter++;
            } while (Tasks.FindByParentIdAndDescription(parentId, newTaskName) != null);
            return newTaskName;
        }

		internal void EditSelectedTaskDescription()
		{
			treeView.LabelEdit = true;
			treeView.SelectedItems[0].BeginEdit();
		}

		private void treeView_AfterLabelEdit(object sender, TreeListViewLabelEditEventArgs e)
		{
			treeView.LabelEdit = false;
			Task row = Tasks.FindById(Convert.ToInt32(e.Item.Tag));
			if (row != null)
			{
				if (e.Label == null || e.Label == String.Empty)
				{
                    e.Cancel = true;
					return;
				}

				row.Description = e.Label;
				try
				{
					Tasks.UpdateTask(row);
				}
				catch (ApplicationException aex)
				{
					e.Cancel = true;
					MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show("This task has been deleted.", this.ParentForm.Text, MessageBoxButtons.OK,
				                MessageBoxIcon.Information);
			}
		}


        internal void DeleteSelectedTask()
        {
            if (MessageBox.Show(
                    "All tasks and sub-tasks assigned to this task will be deleted too. \nAre you sure you want to delete '" +
                    this.treeView.SelectedItems[0].Text + "'?",
                    this.ParentForm.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.DefaultDesktopOnly)
                == DialogResult.OK)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Tasks.DeleteTask((int)treeView.SelectedItems[0].Tag);
                }
                catch (ApplicationException aex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void AddChildNodes(Task parentRow, TreeListViewItem nodeParent)
        {
            Task[] childsRows = Tasks.GetChildTasks(parentRow.Id);
            foreach (Task row in childsRows)
            {
                if (row.Id == Tasks.IdleTask.Id)
                    continue;
                TreeListViewItem nodeChild = CreateNode(row);
                nodeParent.Items.Add(nodeChild);
                AddChildNodes(row, nodeChild);
            }
        }

        private static TreeListViewItem CreateNode(Task task)
        {
            TreeListViewItem node = new TreeListViewItem(task.Description, task.IconId);
            node.Tag = task.Id;
            return node;
        }

        private TreeListViewItem FindTaskNode(int taskId)
        {
            return FindNode(taskId, this.treeView.Items);
        }

        private TreeListViewItem FindNode(int taskId, TreeListViewItemCollection nodes)
        {
            foreach (TreeListViewItem node in nodes)
            {
                if ((int)node.Tag == taskId)
                {
                    return node;
                }
                else
                {
                    if (node.Items.Count > 0)
                    {
                        TreeListViewItem childnode = FindNode(taskId, node.Items);
                        if (childnode != null)
                            return childnode;
                    }
                }
            }
            return null;
        }


		internal int SelectedTaskId
		{
			get { return currentSelectedTask; }
			set
			{
				if (currentSelectedTask == value)
					return;
				TreeListViewItem node;
				node = FindTaskNode(value);
				if (node == null)
					return;
				currentSelectedTask = value;
			    node.Selected = true;
				
				if (this.SelectedTaskChanged != null)
				{
					this.SelectedTaskChanged(this, new EventArgs());
				}
			}
		}

        void treeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.treeView.SelectedItems.Count<=0)
            {
                return;
            }
            if (currentSelectedTask != (int)this.treeView.SelectedItems[0].Tag)
            {
                currentSelectedTask = (int)this.treeView.SelectedItems[0].Tag;
                if (this.SelectedTaskChanged != null)
                {
                    this.SelectedTaskChanged(sender, e);
                }
            }
        }

        #region Drag And Drop

        private Timer timer = new Timer();
        private ImageList imageListDrag = new ImageList();
        private TreeListViewItem dragNode = null;
        private TreeListViewItem tempDropNode = null;

        [DllImport("comctl32.dll")]
        internal static extern bool InitCommonControls();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImageList_BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        internal static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImageList_DragLeave(IntPtr hwndLock);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImageList_DragShowNolock([MarshalAs(UnmanagedType.Bool)]bool fShow);

        private const int TREE_VIEW_INDENT = 19;

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Get drag node and select it
            this.dragNode = (TreeListViewItem)e.Item;
            this.dragNode.Selected = true;
            // Reset image list used for drag image
            this.imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize =
            new Size(Math.Min(this.dragNode.Bounds.Size.Width + TREE_VIEW_INDENT, 256), this.dragNode.Bounds.Height);


            // Create new bitmap
            // This bitmap will contain the tree node image to be dragged
            Bitmap bmp = new Bitmap(Math.Min(this.dragNode.Bounds.Width + TREE_VIEW_INDENT, 256), this.dragNode.Bounds.Height);

            // Get graphics from bitmap
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                // Draw node icon into the bitmap
                gfx.DrawImage(IconsManager.IconsList.Images[dragNode.ImageIndex], 0, 0);

                // Draw node label into bitmap
                gfx.DrawString(this.dragNode.Text,
                               this.treeView.Font,
                               new SolidBrush(this.treeView.ForeColor),
                               TREE_VIEW_INDENT, 1.0f);
            }

            // Add bitmap to imagelist
            this.imageListDrag.Images.Add(bmp);

            // Get mouse position in client coordinates
            Point p = this.treeView.PointToClient(MousePosition);
            // Compute delta between mouse position and node bounds
            //			int dx = p.X + this.treeView.Indent - this.dragNode.Bounds.Left;
            //			int dy = p.Y - this.dragNode.Bounds.Top;

            int dx = p.X - this.dragNode.GetBounds(ItemBoundsPortion.Label).Left - this.treeView.Location.X;
            int dy = p.Y - this.dragNode.Bounds.Top - this.treeView.Location.Y;

            // Begin dragging image
            if (ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
            {
                // Begin dragging
                this.treeView.DoDragDrop(bmp, DragDropEffects.Move);
                // End dragging image
                ImageList_EndDrag();
            }
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // Compute drag position and move image
            Point formP = this.PointToClient(new Point(e.X, e.Y));
            ImageList_DragMove(formP.X - this.treeView.Left, formP.Y - this.treeView.Top);
            
            // Get actual drop node
            TreeListViewItem dropNode = this.treeView.GetItemAt(this.treeView.PointToClient(new Point(e.X, e.Y)));
            if (dropNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;

            // if mouse is on a new node select it
            if (this.tempDropNode != dropNode)
            {
                ImageList_DragShowNolock(false);

                if (tempDropNode != null)
                {
                    this.treeView.Refresh();                  
                }
                dropNode.DrawInsertionLine();

                ImageList_DragShowNolock(true);
                tempDropNode = dropNode;
            }

            // Avoid that drop node is child of drag node 
            TreeListViewItem tmpNode = dropNode;
            while (tmpNode.Parent != null)
            {
                if (tmpNode.Parent == this.dragNode) e.Effect = DragDropEffects.None;
                tmpNode = tmpNode.Parent;
            }
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Unlock updates
            ImageList_DragLeave(this.treeView.Handle);

            // Get drop node
            TreeListViewItem dropNode = this.treeView.GetItemAt(this.treeView.PointToClient(new Point(e.X, e.Y)));

            // If drop node isn't equal to drag node, add drag node as child of drop node
            if (this.dragNode != dropNode)
            {
                // Remove drag node from parent
                if (this.dragNode.Parent == null)
                {
                    this.treeView.Items.Remove(this.dragNode);
                }
                else
                {
                    this.dragNode.Parent.Items.Remove(this.dragNode);
                }

                // Add drag node to drop node
                dropNode.Items.Add(this.dragNode);
                dropNode.Expand();

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Tasks.UpdateParentTask((int)this.dragNode.Tag, (int)dropNode.Tag);
                }
                catch (ApplicationException aex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(aex.Message, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

                // Set drag node to null
                this.dragNode = null;

                // Disable scroll timer
                this.timer.Enabled = false;
            }
            this.treeView.Refresh(); 
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            ImageList_DragEnter(this.treeView.Handle, e.X - this.treeView.Left,
                                e.Y - this.treeView.Top);

            // Enable timer for scrolling dragged item
            this.timer.Enabled = true;
        }

        private void treeView_DragLeave(object sender, EventArgs e)
        {
            ImageList_DragLeave(this.treeView.Handle);
            this.treeView.Refresh();
            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
        }

        private void treeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                e.UseDefaultCursors = false;
                this.treeView.Cursor = Cursors.Default;
            }
            else e.UseDefaultCursors = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // get node at mouse position
            Point pt = treeView.PointToClient(MousePosition);
            TreeListViewItem node = this.treeView.GetItemAt(pt);
            if (node == null) return;

            // if mouse is near to the top, scroll up
            if (pt.Y < 30)
            {
                // set actual node to the upper one
                if (node.PrevVisibleItem != null)
                {
                    node = node.PrevVisibleItem;

                    // hide drag image
                    ImageList_DragShowNolock(false);
                    // scroll and refresh
                    node.EnsureVisible();
                    this.treeView.Refresh();
                    // show drag image
                    ImageList_DragShowNolock(true);
                }
            }
            // if mouse is near to the bottom, scroll down
            else if (pt.Y > this.treeView.Size.Height - 30)
            {
                if (node.NextVisibleItem != null)
                {
                    node = node.NextVisibleItem;

                    ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.treeView.Refresh();
                    ImageList_DragShowNolock(true);
                }
            }
        }

        #endregion

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			this.DeleteSelectedTask();
		}

		private void mnuRename_Click(object sender, EventArgs e)
		{
			this.EditSelectedTaskDescription();
		}

		public void ShowPropertiesSelectedTask()
		{
			TaskPropertiesForm pf;
			pf = new TaskPropertiesForm((int) treeView.SelectedItems[0].Tag);
			pf.ShowDialog(this);
		}

		private void mnuProperties_Click(object sender, EventArgs e)
		{
			ShowPropertiesSelectedTask();
		}

		private void treeView_DoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
        }


        #region Framework Events

        private void Tasks_TasksRowChanged(Tasks.TaskChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                TreeListViewItem nodeParent = FindTaskNode(e.Task.ParentId);
                TreeListViewItem nodeChild = CreateNode(e.Task);
                nodeParent.Items.Add(nodeChild);
                return;
            }
            else if (e.Action == DataRowAction.Change)
            {
                TreeListViewItem node = FindTaskNode(e.Task.Id);
                node.Text = e.Task.Description;
            }
        }

        private void Tasks_TasksRowDeleting(Tasks.TaskChangeEventArgs e)
        {
            TreeListViewItem node = FindTaskNode(e.Task.Id);
            if (node != null && node.ListView != null)
                node.Remove();
        }
        #endregion

    }
}