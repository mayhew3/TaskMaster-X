using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using Auth0.OidcClient;
using System.Text;
using System.Diagnostics;


namespace TaskMaster
{
    public partial class TaskListViewController : UITableViewController
    {
        public List<TodoItem> Items { get; set; }

        static NSString todoItemCellId = new NSString("TodoItemCell");

        private Auth0Client _client;

        public TaskListViewController (IntPtr handle) : base (handle)
        {
            TableView.RegisterClassForCellReuse(typeof(UITableViewCell), todoItemCellId);
            TableView.Source = new TodoItemDataSource(this);
            Items = new List<TodoItem>();
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            LoginButton.TouchUpInside += LoginButton_Clicked;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Login click detected.");

            _client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "mayhew3.auth0.com",
                ClientId = "JVVVYf4nBixusIjWYFpL9zdncnQXybFs",
                Scope = "openid profile"
            });

            var loginResult = await _client.LoginAsync();

            Debug.WriteLine("Auth0 message returned.");

            var sb = new StringBuilder();

            if (loginResult.IsError)
            {
                sb.AppendLine("An error occurred during login:");
                sb.AppendLine(loginResult.Error);
            }
            else
            {
                sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
                sb.AppendLine($"Access Token: {loginResult.AccessToken}");
                sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");
                sb.AppendLine();
                sb.AppendLine("-- Claims --");
                foreach (var claim in loginResult.User.Claims)
                {
                    sb.AppendLine($"{claim.Type} = {claim.Value}");
                }

                Items = await AppDelegate.TodoItemManager.GetTasksAsync();
                TableView.ReloadData();
            }

            Debug.WriteLine(sb.ToString());
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