// primer ejercicio con three.js

var camera, scene, renderer;
var cameraControls;
var clock = new THREE.Clock();
var ambientLight, light;

function init() {
    var canvasWidth = window.innerWidth * 0.9;
    var canvasHeight = window.innerHeight * 0.9;

    // CAMERA
    camera = new THREE.PerspectiveCamera( 45, window.innerWidth / window.innerHeight, 1, 80000 );
    camera.position.set(-1, 1, 3);
    camera.lookAt(0, 0, 0);

    // LIGHTS
    light = new THREE.DirectionalLight( 0xFFFFFF, 0.7 );
    light.position.set( 1, 1, 1 );
    light.target.position.set(0, 0, 0);
    light.target.updateMatrixWorld();

    var ambientLight = new THREE.AmbientLight( 0x111111 );

    // RENDERER
    renderer = new THREE.WebGLRenderer( { antialias: true } );
    renderer.setSize( canvasWidth, canvasHeight );
    renderer.setClearColor( 0xAAAAAA, 1.0 );

    renderer.gammaInput = true;
    renderer.gammaOutput = true;

    // Add to DOM
    var container = document.getElementById('container');
    container.appendChild( renderer.domElement );

    // CONTROLS
    cameraControls = new THREE.OrbitControls( camera, renderer.domElement );
    cameraControls.target.set(0, 0, 0);

    // CREAR UNA ESFERA CON COLOR LLENO

    var radius = .5;   // Radio de la esfera
    var widthSegments = 32;  // Segmentos en la dirección longitudinal
    var heightSegments = 16; // Segmentos en la dirección latitudinal

    // Crear la geometría de la esfera
    var sphereGeometry = new THREE.SphereGeometry(radius, widthSegments, heightSegments); //malla de triángulos

    // Material de la esfera (usando MeshLambertMaterial para compatibilidad)
    var material = new THREE.MeshLambertMaterial({
        color: 0xFF0000,  // Color sólido (rojo)
        // color: 0x3E7BBB,
				// wireframe: true, 
        side: THREE.DoubleSide // Para asegurar que la esfera sea visible desde ambos lados
    });

    // Crear el objeto Mesh (esfera) con la geometría y material
    var sphere = new THREE.Mesh(sphereGeometry, material);

    // SCENE
    scene = new THREE.Scene();
    scene.add(light);
    scene.add(ambientLight);
    scene.add(sphere);  // Agregar la esfera a la escena
}

function animate() {
    window.requestAnimationFrame(animate);
    render();
}

function render() {
    var delta = clock.getDelta();
    cameraControls.update(delta);
    renderer.render(scene, camera);
}

try {
    init();
    animate();
} catch (e) {
    var errorReport = "Your program encountered an unrecoverable error, can not draw on canvas. Error was:<br/><br/>";
    $('#container').append(errorReport + e);
}
