using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.EF;
using TODOList.DAL.Entities;

namespace TODOList.DAL.Repositories
{
    class StepRepository : IRepository<Step>
    {
        private TODOContext db;

        public StepRepository(TODOContext context)
        {
            this.db = context;
        }

        public IEnumerable<Step> GetAll()
        {
            return db.Steps;
        }

        public Step Get(int id)
        {
            return db.Steps.Find(id);
        }

        public void Create(Step step)
        {
            db.Steps.Add(step);
        }

        public void Update(Step step)
        {
            db.Entry(step).State = EntityState.Modified;
        }

        public IEnumerable<Step> Find(Func<Step, Boolean> predicate)
        {
            return db.Steps.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Step step = db.Steps.Find(id);
            if (step != null)
            {
                db.Steps.Remove(step);
            }
        }
    }
}
