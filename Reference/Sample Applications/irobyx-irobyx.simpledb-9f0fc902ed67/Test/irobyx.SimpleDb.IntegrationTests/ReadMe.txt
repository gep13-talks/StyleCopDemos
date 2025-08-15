To be able to run the integration tests:
Add a file called "amazon.config" and add the following appSettings using the correct keyvalues.

<?xml version="1.0" encoding="utf-8" ?>
 <appSettings>
    <add key="AWSAccessKey" value="AWSAccessKey"/>
    <add key="AWSSecretKey" value="AWSSecretKey"/>
    <add key="SimpleDbRegion" value="https://sdb.eu-west-1.amazonaws.com"/> 
  </appSettings>