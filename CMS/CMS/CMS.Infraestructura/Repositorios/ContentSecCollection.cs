﻿using CMS.Dominio.Entidades;
using CMS.Infraestructura.Data;
using CMS.Aplicacion.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CMS.Infraestructura.Repositorios
{
    public class ContentSecCollection : IContentSecAppService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private readonly IMongoCollection<ContentSec> collection;

        public ContentSecCollection()
        {
            collection = _repository.db.GetCollection<ContentSec>("ContentSec");
        }
        public async Task Delete(string id)
        {
            var filter = Builders<ContentSec>.Filter.Eq(s => s.ContentSec_Id, new ObjectId(id));
            await collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<ContentSec>> GetAll()
        {
            return await collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task Insert(ContentSec model)
        {
            await collection.InsertOneAsync(model);
        }

        public async Task Update(ContentSec model)
        {
            var filter = Builders<ContentSec>
                .Filter
                .Eq(s => s.ContentSec_Id, model.ContentSec_Id);
            await collection.ReplaceOneAsync(filter, model);
        }

		public async Task<ContentSec> GetById(string id)
		{
			return await collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
				.Result.FirstAsync();
		}
	}
}
