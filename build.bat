docker build -t jrspersonalloans .
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 283410898245.dkr.ecr.us-east-1.amazonaws.com
docker tag jrspersonalloans:latest 283410898245.dkr.ecr.us-east-1.amazonaws.com/jrspersonalloans:latest
docker push 283410898245.dkr.ecr.us-east-1.amazonaws.com/jrspersonalloans:latest
workstation id=jrspersonalloans.mssql.somee.com;packet size=4096;user id=jrosario19_SQLLogin_1;pwd=1eq6aqxw8q;data source=jrspersonalloans.mssql.somee.com;persist security info=False;initial catalog=jrspersonalloans
