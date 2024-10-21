#!/bin/bash
####
# Docker management script for Eventstore Training Classes
#####

####
# Check to see if instance is currently running
# If instance is running kill it and start fresh instance
########

#############
# Some bash notes for the curious
# ps --format returns just the names rather than the verbose table format
# grep -q is search in silent mode to prevent output to the terminal
# echo -e the -e allows echo of newline characters
#######
echo "Starting EventStoreDB docker container..."

# Function to check if Docker daemon is running by checking the output of 'docker ps'
check_docker() {
  docker ps > /dev/null 2>&1
}

# Initial check before entering the loop
if ! check_docker; then
       echo "Docker daemon is not running. Awaiting for it to start..."
       # If Docker daemon is not available, start retry loop
       max_attempts=10
       attempt=0
       while ! check_docker; do
              attempt=$((attempt+1))
              if [ "$attempt" -ge "$max_attempts" ]; then
                     echo "Docker daemon is still not available. Exiting"
                     exit 1
              fi
              echo "Retrying... (Attempt $attempt of $max_attempts)"
              sleep 5
       done
       
       echo "Docker daemon is now running. Proceeding with the rest of the script..."
fi

# Check to see if docker container is already running
# If yes, kill it and restart
# If no, download and start
if docker ps -a --format '{{.Names}}'| grep -q esdb;
then 
       echo -e '\nesdb docker container appears to be running\n';

       # Kill the container
       docker stop esdb-node

       ## Remove the instance
       docker rm esdb-node

       docker run -d --name esdb-node -it -p 2113:2113 -p 1113:1113 \
              eventstore/eventstore:lts --insecure --run-projections=All \
              --enable-external-tcp --enable-atom-pub-over-http

else
       docker run -d --name esdb-node -it -p 2113:2113 -p 1113:1113 \
              eventstore/eventstore:lts --insecure --run-projections=All \
              --enable-external-tcp --enable-atom-pub-over-http
fi

# Grab URL to EventStoreDB Admin UI
ESDB_URL=http://localhost:2113
if [ "$CODESPACES" == "true" ]
then
       # Grab URL from github codespaces if script is ran from there
       ESDB_URL=https://"$CODESPACE_NAME"-2113.$GITHUB_CODESPACES_PORT_FORWARDING_DOMAIN
fi

# Print URL to EventStoreDB Admin UI
echo ""
echo ""
echo -e "🚀 \e[32mEventStoreDB Server has started!!\e[0m 🚀" 
echo ""
echo -e "Browse the EventStoreDB Admin UI at 👉 \e[0m \e[34m$ESDB_URL\e[0m"
echo ""
echo ""
