
ALTER TABLE [config].[Report]
ADD IsCacheEnable bit not null default 0;

ALTER TABLE [config].[Report]
ADD CacheAliveTime int null;