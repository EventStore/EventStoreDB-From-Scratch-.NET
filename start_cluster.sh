#!/bin/bash
###########################################################
#
# Docker management script for Eventstore Training Classes
#
###########################################################

#################################################################################
# Some bash notes for the curious
# docker ps --format returns just the names rather than the verbose table format
# grep -q is search in silent mode to prevent output to the terminal
# echo -e the -e allows echo of newline characters
#################################################################################

echo "Starting EventStoreDB docker container. This can take a moment..."

##################################################
#
# Step 1. Wait for the docker service to start up
#
##################################################

max_attempts=10
attempt=0
while ! docker ps > /dev/null 2>&1; do                                # While docker fails to run (e.g. Docker daemon is not running)
       if [ "$attempt" -ge "$max_attempts" ]; then                    # If number of attempt exceeds the max_attempts then we exit
              echo "Docker daemon is still not available. Exiting"
              exit 1
       fi
       attempt=$((attempt+1))                                         # Increment the attempt count
       sleep 2                                                        # Wait for few seconds before we check again
done

#####################################################################################
#
# Step 2. Stop and remove the EventStoreDB docker container if it is already running
#
#####################################################################################

if docker inspect esdb-node > /dev/null 2>&1; then                            # If the EventStoreDB docker container (esdb-node) is running
       echo -e 'EventStoreDB docker container appears to be running...';
       echo -e 'Stopping and removing the EventStoreDB docker container...';

       docker rm -f esdb-node > /dev/null 2>&1                                # Remove the docker container
fi

################################################################################################################################################
#
# Step 3. Run the EventStoreDB docker container
#
# docker run                        # Start a new Docker container using the 'docker run' command
#      -d \                         # Run the container in detached mode (in the background)
#      --name esdb-node \           # Assign the container a name ('esdb-node' in this case)
#      -p 2113:2113 \               # Map port 2113 on the host to port 2113 in the docker container. Required for the EventStoreDB Admin UI
#      eventstore/eventstore:lts \  # Specify the Docker image to use, in this case, the EventStoreDB long-term support version (lts)
#      --insecure \                 # Run EventStoreDB in insecure mode, without authentication and SSL/TLS security (usually for development)
#      --run-projections=All \      # Enable all projections in EventStoreDB, including system and user projections
#      --enable-atom-pub-over-http  # Enable the AtomPub API over HTTP. Required for the EventStoreDB Admin UI
#
################################################################################################################################################

echo -e 'Starting the EventStoreDB docker container...';

docker run \
       -d \
       --name esdb-node \
       -p 2113:2113 \
       eventstore/eventstore:lts \
       --insecure \
       --run-projections=All \
       --enable-atom-pub-over-http

if ! docker inspect esdb-node > /dev/null 2>&1; then    # If the EventStoreDB docker container (esdb) is running, exit
       echo "The EventStoreDB docker container is not running. Exiting."
       exit 1
fi

######################################################################################
#
# Step 4. Print success message if EventStoreDB docker container started successfully
#
######################################################################################

ESDB_URL=http://localhost:2113                                                            # Set default URL to localhost (for EventStoreDB started locally, not in Codespaces)
if [ "$CODESPACES" == "true" ]; then                                                      # If this environment is Codespaces 
       ESDB_URL=https://"$CODESPACE_NAME"-2113.$GITHUB_CODESPACES_PORT_FORWARDING_DOMAIN  # Build the URL to forwarded github codespaces domain       
fi

echo ""
echo ""
echo -e "🚀 \e[32mEventStoreDB Server has started!!\e[0m 🚀" 
echo ""
echo -e "URL to EventStoreDB Admin UI 👉 \e[0m \e[34m$ESDB_URL\e[0m"                      # Print URL to EventStoreDB Admin UI
echo ""
echo ""