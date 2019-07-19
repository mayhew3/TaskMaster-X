using Foundation;
using System;
using UIKit;
using System.Collections.Generic;


namespace TaskMaster
{
    public partial class TaskListViewController : UITableViewController
    {
        public List<TodoItem> Items { get; set; }

        static NSString todoItemCellId = new NSString("TodoItemCell");

        public TaskListViewController (IntPtr handle) : base (handle)
        {
            TableView.RegisterClassForCellReuse(typeof(UITableViewCell), todoItemCellId);
            /*
            TodoItem todoItem = new TodoItem();
            todoItem.id = 1;
            todoItem.name = "Temp";
            todoItem.person_id = 1;
            todoItem.date_added = new DateTime();
            
            Items.Add(todoItem);
            */
            TableView.Source = new TodoItemDataSource(this);
            Items = new List<TodoItem>();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Items = await AppDelegate.TodoItemManager.GetTasksAsync();
            TableView.ReloadData();
        }

        class TodoItemDataSource : UITableViewSource
        {
            TaskListViewController controller;

            public TodoItemDataSource(TaskListViewController controller)
            {
                this.controller = controller;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(TaskListViewController.todoItemCellId);

                int row = indexPath.Row;
                cell.TextLabel.Text = controller.Items[row].name;
                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return controller.Items.Count;
            }
        }
    }
}