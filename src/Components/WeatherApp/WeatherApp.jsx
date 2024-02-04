import React from 'react';
import styled from "styled-components";
import { MdOutlineSearch } from "react-icons/md";
import { WiCloud } from "react-icons/wi";
import { WiHumidity } from "react-icons/wi";
import { FaCity } from "react-icons/fa";
import { FaWind } from "react-icons/fa6";

const SearchBar = styled.input`
    font-size: 2rem;
    margin-top: 10px;
    border-radius: 1rem;
`;

const Container = styled.div`
    width: 50%;
    height: 100%;
    margin: auto;
    background-color: color-mix(in lch, plum, pink);
    border-radius: 12px;
    margin-top: 75px;
`;

const MdOutlineSearchStyle = styled(MdOutlineSearch)`
    font-size: 1.5rem;
    color: white;
    margin-left: 1rem;
`;

const CityName = styled.h1`
    color: white;
`;

const CityNameDiv = styled.div`
    display: inline-flex;
`;

const HumidityDiv = styled.div`
    display: flex;
    justify-content: center;
`;

const WiCloudStyle = styled(WiCloud)`
    font-size: 3rem;
    color: white;
`;

const WiHumidityStyle = styled(WiHumidity)`
    font-size: 3rem;
    color: white;
    margin-block: auto;
`;

const FaCityStyle = styled(FaCity)`
    font-size: 2rem;
    color: white;
    margin-block: auto;
    margin-right: 10px;
`;

const WindStyle = styled(FaWind)`
    font-size: 2rem;
    color: white;
    margin-block: auto;
    margin-right: 10px;
`

const WeatherApp = () => {

    let api_key = "4ec0a2035b5e3cacc4b104c807f2e83b";

    const search = async () => {
        const checkSerachname = document.getElementById("searchName");
        if (checkSerachname.value === "") {
            return 0;
        }
        let url = `https://api.openweathermap.org/data/2.5/weather?q=${checkSerachname.value}&units=Metric&appid=${api_key}`
        let response = await fetch(url);
        let data = await response.json();

        const locationElement = document.getElementById("location");
        locationElement.innerHTML = data.name;
        const humidityElement = document.getElementById("humidity");
        humidityElement.innerHTML = data.main.humidity;
        const windElement = document.getElementById("wind");
        windElement.innerHTML = data.wind.speed;
    }

    return (
        <Container>
            <WiCloudStyle></WiCloudStyle>
            <div>
                <SearchBar type='text' placeholder='search' id='searchName' />
                <MdOutlineSearchStyle onClick={() => { search() }} />
            </div>
            <CityNameDiv>
                <FaCityStyle/>
                <CityName>Area Name:</CityName>
                <CityName id='location'>Columbus</CityName>
            </CityNameDiv>
            <HumidityDiv>
                <WiHumidityStyle/>
                <CityName>Humidity: </CityName>
                <CityName id='humidity'>18</CityName>
                <CityName>%</CityName>                
            </HumidityDiv>
            <HumidityDiv>
                <WindStyle/>
                <CityName>Wind: </CityName>
                <CityName id='wind'>2</CityName>
                <CityName>km/h</CityName>          
            </HumidityDiv>
            
        </Container>
    )
}

export default WeatherApp;
