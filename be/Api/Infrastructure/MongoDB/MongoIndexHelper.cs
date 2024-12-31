namespace Api.Infrastructure.MongoDB;

public static class MongoIndexHelper
{
    public static void CreateIndex<T>(this IMongoDatabase database, CreateIndexModel<T> createIndexModel)
    {
        var collection = database.GetCollection<T>(typeof(T).Name);

        collection.Indexes.CreateOne(createIndexModel);
    }

    public static void CreateIndices(IMongoDatabase database)
    {
    }
}
