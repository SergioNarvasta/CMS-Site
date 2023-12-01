﻿
using CMS.Infraestructura.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using CyanCMS.Infraestructure.Interfaces;
using CyanCMS.Domain.Entities;

namespace SmartCMS.Infraestructure.Services
{
    public class CompanyCollection : ICompanyService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<Company> collection;

        public CompanyCollection()
        {
            collection = _repository.db.GetCollection<Company>("Company");
        }
        public async Task Delete(string id)
        {
            var filter = Builders<Company>.Filter.Eq(s => s.Company_Id, new ObjectId(id));
            await collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await collection.FindAsync(new BsonDocument { { "Company_Estado", 1 } }
                ).Result.ToListAsync();
        }

        public async Task Insert(Company model)
        {
            await collection.InsertOneAsync(model);
        }

        public async Task Update(Company model)
        {
            var filter = Builders<Company>
                .Filter
                .Eq(s => s.Company_Id, model.Company_Id);
            await collection.ReplaceOneAsync(filter, model);
        }

        public async Task<Company> GetById(string User_Pk)
        {
            return await collection.FindAsync(new BsonDocument { { "User_Pk", User_Pk } })
                .Result.FirstOrDefaultAsync();
        }
    }
}
