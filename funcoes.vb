Imports Npgsql
Imports System.Data
Imports System

Module funcoes
    Dim conn As NpgsqlConnection = Nothing
    Dim ConexaoPG As String = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=cavassin12;Database=postgres"
    Sub AbreConexao()
        
        Try

            conn = New NpgsqlConnection(ConexaoPG)
            conn.Open()
        Catch ex As Exception
            MensagemErro("Erro para abrir a conexao - " & ex.Message, "")
        End Try
        
    End Sub


    Sub FechaConexao()
        conn.Close()
    End Sub
    
    Function ExecutarSQL(sql)
        AbreConexao()
        Dim da As Npgsql.NpgsqlDataAdapter = New NpgsqlDataAdapter(sql, conn)
        Return da
        FechaConexao()
    End Function

        
    Function MontaTabela(sql)

        Dim da As Npgsql.NpgsqlDataAdapter = ExecutarSQL(sql)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        Return ds.Tables(0).DefaultView

    End Function
    Function Crud(sql)
        Dim conn As NpgsqlConnection = Nothing
        conn = New NpgsqlConnection(ConexaoPG)
        Dim comando As NpgsqlCommand = New NpgsqlCommand
        comando.Connection = conn
        conn.Open()
        comando.CommandText = sql
        Try
            comando.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
        FechaConexao()

    End Function
    Function VoltaUmReg(sql)
        Dim conn As NpgsqlConnection = Nothing
        Dim dt As New DataTable
        Dim ds As DataSet = New DataSet
        conn = New NpgsqlConnection(ConexaoPG)
        conn.Open()
        Dim comando As NpgsqlCommand = New NpgsqlCommand
        comando.Connection = conn
        Dim da As New NpgsqlDataAdapter(comando)
        comando.CommandText = sql
        comando.ExecuteNonQuery()

        Return 0

        FechaConexao()

    End Function
    Function ContarTotal(sql)
        Dim conn As NpgsqlConnection = Nothing
        Dim dt As New DataTable
        conn = New NpgsqlConnection(ConexaoPG)
        conn.Open()
        Dim comando As NpgsqlCommand = New NpgsqlCommand
        comando.Connection = conn
        Dim da As New NpgsqlDataAdapter(comando)
        comando.CommandText = sql
        comando.ExecuteNonQuery()
        da.Fill(dt)
        Return dt.Rows.Count
        FechaConexao()

    End Function

    Sub MensagemErro(mensagem, titulo)
        If titulo.ToString.Length = 0 Then
            titulo = "Gerenciador"
        End If
        MsgBox(mensagem, MsgBoxStyle.Critical, titulo)
    End Sub
    Sub MensagemSucesso(mensagem, titulo)
        If titulo.ToString.Length = 0 Then
            titulo = "Gerenciador"
        End If
        MsgBox(mensagem, MsgBoxStyle.Information, titulo)
    End Sub
    Sub MensagemAviso(mensagem, titulo)
        If titulo.ToString.Length = 0 Then
            titulo = "Gerenciador"
        End If
        MsgBox(mensagem, MsgBoxStyle.Exclamation, titulo)
    End Sub


End Module
