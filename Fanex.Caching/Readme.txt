Couchbase configuration:
    <?xml version="1.0"?>
    <configuration>
      <configSections>
        <section name="couchbase" type="Couchbase.Configuration.CouchbaseClientSection, Couchbase"/>
      </configSections>
      <couchbase>
        <servers bucket="default" bucketPassword="">
          <add uri="http://10.18.200.201:8091/pools/default"/>
          <add uri="http://10.18.200.207:8091/pools/default"/>
          <add uri="http://10.18.200.210:8091/pools/default"/>
        </servers>
      </couchbase>
    </configuration>