import os
import json


         
#Add your customized code here         
def get_config():   
    with open(os.path.dirname(__file__) + os.sep + "config.json", 'r') as f:
        startup = json.load(f)
	
    return startup
	
def get_configA(config):
	with open(os.path.dirname(__file__) + os.sep + config, 'r') as ff:
		startupa = json.load(ff)
	return startupa