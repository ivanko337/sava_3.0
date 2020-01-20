using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sava3._0.Infrastructure
{
    public static class DBService
    {
        public static void AddNewEntity<T>(Window wnd, T newEntity, DBContext context, DbSet<T> set) where T : class
        {
            context.Entry(newEntity).State = EntityState.Added;

            set.Add(newEntity);

            SaveChanges(wnd, context);
        }

        public static void UpdateEntity<T>(Window wnd, T entity, DBContext context)
        {
            context.Entry(entity).State = EntityState.Modified;

            SaveChanges(wnd, context);
        }

        private static void SaveChanges(Window wnd, DBContext context)
        {
            try
            {
                context.SaveChanges();

                wnd.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //wnd.DialogResult = false;
                throw;
            }
        }
    }
}
