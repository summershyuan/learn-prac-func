namespace LearnPrac.Function
{
    public class TelemetryItem
    {
        private double temperature;
        private double pressure;
        private bool isNormalPressure;
        private status temperatureStatus;    
        public TelemetryItem(double temperature, double pressure)
        {
            this.temperature = temperature;
            this.pressure = pressure;
        }

        public double getTemperature(){
            return temperature;
        }

        public double getPressure() {
            return pressure;
        }

        public string toString(){
            return "TelemetryItem={temperature="
                + temperature + ",pressure=" + pressure + "}";
        }

        public bool checkPressure(){
            return isNormalPressure;
        }

        public void setNormalPressure(bool isNormal){
            this.isNormalPressure = isNormal;
        }

        public status getTemperatureStatus() {
            return temperatureStatus;
        }

        public void setTemperatureStatus(status temperatureStatus) {
            this.temperatureStatus = temperatureStatus;
        }

    }

    public enum status
    {
        COOL,
        WARM,
        HOT
    }


}