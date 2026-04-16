# 🔧 INSTRUÇÕES PARA RESOLVER O ERRO DE ENTITY FRAMEWORK

## ✅ O QUE FOI FEITO

Seu arquivo `GestaoAcademica.csproj` foi **convertido** de:
- ❌ .NET Framework 4.7.2 (formato antigo)
- ✅ .NET 10.0 (formato SDK-style moderno)

Agora ele suporta Entity Framework Core 8.0!

---

## 📋 PRÓXIMOS PASSOS

### Passo 1: Feche o Visual Studio
```
File → Close (Salvar todas as mudanças)
```

### Passo 2: Reabra a Solução
```
File → Open → GestaoAcademica.sln
```

**Aguarde:**
- Visual Studio vai detectar a nova configuração
- Vai restaurar os pacotes NuGet automaticamente
- Pode levar 1-2 minutos

### Passo 3: Aguarde a restauração de pacotes
```
Output Window → Restore started...
Output Window → Restore completed (depois de alguns segundos)
```

### Passo 4: Compile
```
Build → Rebuild Solution (Ctrl + Shift + B)
```

**Resultado esperado:**
```
========== Rebuild All: 1 succeeded, 0 failed ==========
```

### Passo 5: Agora execute Update-Database
```
Tools → NuGet Package Manager → Package Manager Console

PM> Update-Database -Verbose
```

**Resultado esperado:**
```
The EF Core tools version '8.0.0' is older than the runtime version '8.0.X'.
Update the EF Core tools.

Build started...
Build succeeded.

Migrations will be applied automatically.
```

---

## ⚠️ SE RECEBER ERRO

### Erro: "DbContext not found"
```
Solução:
1. GestaoAcademicaContext.cs existe em Dados/?
   └─ Se não: crie-o
   
2. DbContext herda de Microsoft.EntityFrameworkCore.DbContext?
   └─ Se não: corrija a herança
```

### Erro: "No migrations found"
```
Solução:
1. Delete pasta Migrations/ (se existir)
2. Execute: Add-Migration InitialCreate
3. Depois: Update-Database
```

### Erro: "Connection string not found"
```
Solução:
1. Crie appsettings.json na raiz do projeto:
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=gestao_academica;User=root;Password=;"
  }
}
```

```
2. Configure em Program.cs:
```

```csharp
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
options.UseMySql(connectionString, 
    ServerVersion.AutoDetect(connectionString));
```

---

## ✨ RESUMO DAS MUDANÇAS

| Antes | Depois |
|-------|--------|
| .NET Framework 4.7.2 | .NET 10.0 |
| Formato XML antigo | Formato SDK-style |
| Sem Entity Framework | EF Core 8.0 |
| Sem Testes | xUnit + Moq |
| Manual | NuGet automático |

---

## 🎯 CHECKLIST

- [ ] Fechei Visual Studio
- [ ] Reabrı a solução
- [ ] Aguardei restauração de pacotes
- [ ] Compilei com sucesso
- [ ] Package Manager Console está aberto
- [ ] Executei Update-Database
- [ ] Verifico Migrations/ foi criado
- [ ] Banco de dados foi criado

---

**Continue com a apresentação após estes passos! ✅**
