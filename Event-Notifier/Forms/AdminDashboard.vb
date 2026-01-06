Imports University_EventPlatform.Classes ' Ensures we can access DatabaseHelper

Public Class AdminDashboard
' This method runs automatically when the form opens
    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub

    ' Method to fetch stats from the database and update the Labels (lblTotalEvents, etc.)
    Private Sub LoadDashboardStats()
        Try
            ' In future steps, we will connect this to DatabaseHelper.
            ' For now, we set them to 0 as placeholders.
            lblTotalEvents.Text = "0"
            lblTotalRegistrations.Text = "0"
            
            lblStatus.Text = "Dashboard loaded successfully."
        Catch ex As Exception
            MessageBox.Show("Error loading dashboard: " & ex.Message)
        End Try
    End Sub

    ' LOGIC: Handles the "Logout" menu click
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        ' Ask user if they are sure [cite: 397]
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ' Show the login form again
            Dim loginForm As New AdminLogin()
            loginForm.Show()
            ' Close the dashboard
            Me.Close()
        End If
    End Sub

    ' LOGIC: Handles the "Exit" menu click
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    ' LOGIC: Handles the "Create Event" button click
    Private Sub btnCreateEvent_Click(sender As Object, e As EventArgs) Handles btnCreateEvent.Click
        ' This checks if the Create Event button works. 
        ' We will replace this message with "eventForm.Show()" once we build Form 3.
        MessageBox.Show("Create Event Form coming next!", "Placeholder")
    End Sub
End Class
