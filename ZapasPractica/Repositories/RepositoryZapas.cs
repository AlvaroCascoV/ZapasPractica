using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZapasPractica.Data;
using ZapasPractica.Models;

namespace ZapasPractica.Repositories
{
    public class RepositoryZapas
    {
        private ZapasContext context;
        public RepositoryZapas(ZapasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }
        public async Task<Zapatilla> FindZapatillaAsync(int idzapa)
        {
            return await this.context.Zapatillas.FindAsync(idzapa);
        }
        public async Task<int> GetNumeroImagenesAsync(int idzapa)
        {
            return await this.context.ImagenesZapatillas.Where(x => x.IdProducto == idzapa).CountAsync();
        }
        public async Task<ImagenZapatilla> GetImagenPosicionAsync(int posicion, int idzapa)
        {
            string sql = "SP_IMAGENES_ZAPATILLAS @idzapa, @posicion";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdZapa = new SqlParameter("@idzapa", idzapa);

            var consulta = await this.context.ImagenesZapatillas.FromSqlRaw(sql, pamIdZapa, pamPosicion).ToListAsync();

            ImagenZapatilla imagenZapatilla = consulta.FirstOrDefault();

            return imagenZapatilla;
        }
    }
}
