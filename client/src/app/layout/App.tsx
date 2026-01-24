import { useState } from "react";
import { Box, Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import NavBar from "./NavBar";
import { Outlet } from "react-router-dom";

function App() {
  
  const [darkMode, setDarkMode] = useState(false);
  const palleteType = darkMode ? 'dark' : 'light';
  const theme = createTheme({
    palette:{
      mode: palleteType,
      background:{
        default: (palleteType === 'light') ? '#eaeaea' : '#121212'
      }
    }
  });

  const toggleDarkMode = () => {
    setDarkMode(!darkMode);
  }


  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <NavBar toggleDarkMode={toggleDarkMode} darkMode={darkMode}/>
        <Box
          sx={{minHeight: '100vh', 
          background: darkMode 
            ? 'radial-gradient(circle, #1e3aBa, #111B27)' 
            : 'radial-gradient(circle, #baecf9, #f0f9ff)'
          }}
        >
          <Container maxWidth="xl" sx={{pt: 14}}>
            <Outlet /> {/*when navigating, here outlet will be swap with actual components, as per the routing config */}
          </Container>
        </Box>
    </ThemeProvider>
  )
}

export default App
