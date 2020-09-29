### CONVENÇÕES PARA NOMES DE OBJETOS

|                      |                     Convenção                     |       Anotação       |
|:--------------------:|:-------------------------------------------------:|:--------------------:|
|  Classe para Tabela  | Nome da propriedade DbSet<T>, onde T é a entidade |   [Table("actor")]   |
| Atributo para Coluna |        Nome da propriedade relacionada em T       | [Column("actor_id")] |

### CONVENÇÕES TIPOS CLR PARA SQL SERVER *

|    CLR   |    SQL Server    | 
|:--------:|:----------------:| 
|    int   |        int       | 
|   long   |      bigint      |
| DateTime | datetime2        |
| Guid     | uniqueidentifier |
| bool     | bit              |
| byte     | tinyint          |

|       CLR      |   SQL Server   |
|:--------------:|:--------------:|
|     double     |      float     |
| DateTimeOffset | datetimeoffset |
| short          | smallint       |
| float          | real           |
| decimal        | decimal(18, 2) |
| TimeSpan       | time           |

* informações obtidas da classe SqlServerTypeMapper