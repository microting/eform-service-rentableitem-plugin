#!/bin/bash

if [ ! -d "/var/www/microting/eform-service-rentableitem-plugin" ]; then
  cd /var/www/microting
  su ubuntu -c \
  "git clone https://github.com/microting/eform-service-rentableitem-plugin.git -b stable"
fi

cd /var/www/microting/eform-service-rentableitem-plugin
su ubuntu -c \
"dotnet restore ServiceRentableItemsPlugin.sln"

echo "################## START GITVERSION ##################"
export GITVERSION=`git describe --abbrev=0 --tags | cut -d "v" -f 2`
echo $GITVERSION
echo "################## END GITVERSION ##################"
su ubuntu -c \
"dotnet publish ServiceRentableItemsPlugin.sln -o out /p:Version=$GITVERSION --runtime linux-x64 --configuration Release"

su ubuntu -c \
"mkdir -p /var/www/microting/eform-debian-service/MicrotingService/out/Plugins/"

su ubuntu -c \
"cp -av /var/www/microting/eform-service-rentableitem-plugin/ServiceRentableItemsPlugin/out /var/www/microting/eform-debian-service/MicrotingService/out/Plugins/ServiceRentableItemsPlugin"
/rabbitmqadmin declare queue name=eform-service-rentableitem-plugin durable=true
