Imports Npgsql
Imports System.Configuration 
Imports BCrypt.Net 'I want to add the logic that actually checks the password using BCrypt

Public Class DatabaseHelper
    ' Variable to hold the connection string internally
    Private connectionString As String

    ' Constructor: Runs automatically when you create a New DatabaseHelper()
    Public Sub New()
        ' Fetch the connection string from App.config by its name
        connectionString = ConfigurationManager.ConnectionStrings("UniversityEventsDB").ConnectionString
    End Sub

    ' Method to create and return a new database connection object
    Public Function GetConnection() As NpgsqlConnection
        ' Create a new connection using the string we loaded earlier
        Return New NpgsqlConnection(connectionString)
    End Function

    ' Test method to verify database connectivity
    Public Function TestConnection() As Boolean
        Try
            Using conn As NpgsqlConnection = GetConnection()
                conn.Open() ' Attempt to open the connection
                Return True ' If we get here, it worked!
            End Using ' "Using" automatically closes the connection here
        Catch ex As Exception
            ' If it fails, print the error to the Output window
            System.Diagnostics.Debug.WriteLine("Connection Error: " & ex.Message)
            Return False
        End Try
    End Function

' Here's the updated part of the code    
' Logic to check if username and password are correct
    Public Function ValidateAdmin(username As String, password As String) As Admin
        Dim admin As Admin = Nothing

        Using conn As NpgsqlConnection = GetConnection()
            conn.Open()
            ' parameterized query to prevent SQL Injections
            Dim query As String = "SELECT AdminID, Username, PasswordHash, Email, FullName FROM Admins WHERE Username = @Username"
            
            Using cmd As New NpgsqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Username", username)
                
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Get the hash stored in the database
                        Dim storedHash As String = reader("PasswordHash").ToString()
                        
                        ' Use BCrypt to verify the plain password against the hash
                        If BCrypt.Net.BCrypt.Verify(password, storedHash) Then
                            ' If match, create the Admin object
                            admin = New Admin With {
                                .AdminID = Convert.ToInt32(reader("AdminID")),
                                .Username = reader("Username").ToString(),
                                .Email = reader("Email").ToString(),
                                .FullName = reader("FullName").ToString()
                            }
                        End If
                    End If
                End Using
            End Using
        End Using

        Return admin ' Returns the object if success, or Nothing if failed
    End Function
End Class
