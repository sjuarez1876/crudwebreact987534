import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();
    const [updateEmpleado, setEmpleado] = useState({ idEmpleado: 0, nombre: '', telefono: '', idRol: 1, descripcion: '' });

    useEffect(() => {
        populateWeatherData();
    }, []);

    

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>No.</th>
                    <th>Empleado</th>
                    <th>Telefono</th>
                    <th>Rol</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.idEmpleado}>
                        <td>{forecast.idEmpleado}</td>
                        <td>{forecast.nombre}</td>
                        <td>{forecast.telefono}</td>
                        <td>{forecast.descripcion}</td>
                        <td><button type="button" onClick={() => setEmpleado(forecast)}>Editar</button>
                            <button type="button" onClick={() => eliminar(forecast.idEmpleado)}>Eliminar</button>
                        </td>
                       
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            
            <h3 id="tableLabel">Empleados</h3>
            <p>Gestion empleados</p>
            <div>
                <button type="button" onClick={() => nuevoEmpleado()}>Nuevo</button>
            </div>
            <div>
                Nombre: <input type="text" name="nombre" value={updateEmpleado.nombre} onChange={handleChange}></input>
                Télefono: <input type="text" name="telefono" value={updateEmpleado.telefono} onChange={handleChange}></input>
                Roles: <select name="idRol" value={updateEmpleado.idRol} onChange={handleChange}>
                            <option value="1">Rol 1</option>
                            <option value="2">Rol 2</option>
                      </select>
            </div>
            <div>
                <button type="button" onClick={guardar}>Guardar</button>
            </div>
            {contents}
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('api/Empleados');
        if (response.ok) {
            //console.log(response);
            const data = await response.json();
            setForecasts(data);
        }
    }

    
    function eligeEmpleado(objEmp) {

        setEmpleado(objEmp);

    }

    function nuevoEmpleado() {
        var objEmp = { idEmpleado: 0, nombre: '', telefono: '', idRol: 1, descripcion: '' };
        setEmpleado(objEmp);

    }
    function handleChange(e) {

        const { name, value } = e.target;
        
        setEmpleado((prv) => ({ ...prv, [name]: value }));


    }

    async function guardar() {
        //Guarda

       

        if (updateEmpleado.idEmpleado == 0) {
            const response = await fetch('api/Empleados', { method: 'post', body: JSON.stringify(updateEmpleado), headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' } });
            if (response.ok) {

                const data = await response.json();
                if (data.codigo == '1') {


                    alert('El registro se guardó correctamete');

                    populateWeatherData();
                    nuevoEmpleado();
                }

            }
        } else {

            //Actuliza
            const response = await fetch('api/Empleados/ActualizarEmpleado', { method: 'post', body: JSON.stringify(updateEmpleado), headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' } });            

            if (response.ok) {

                const data = await response.json();
                if (data.codigo == '1') {
                    alert('El registro se actualizó correctamete');
                    populateWeatherData();
                    nuevoEmpleado();
                }
            }
        }
       
    }


    async function eliminar(id) {
        var confirmaEliminar = confirm('Desea eliminar el registro seleccionado?');
        if (confirmaEliminar) {
            const response = await fetch(`api/Empleados/${id}`, { method: 'delete', headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' } });
            if (response.ok) {

                const data = await response.json();
                if (data.codigo == '1') {
                    populateWeatherData();

                }
            }
        }
        
    }


}

export default App;