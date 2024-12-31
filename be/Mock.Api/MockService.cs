
using Common.Models;

namespace Mock.Api;

public static class MockService
{
    public static SensorData GetSensorData(Guid ecosystemId)
    {
        return new SensorData();
    }

    public static AggregateData GetAggregateData(Guid ecosystemId) //TODO: add parameters for day, week, month, year - from and to
    {
        return new AggregateData();
    }
}
