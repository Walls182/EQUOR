using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQUOR.DataContext;
using EQUOR.Models;
using QRCoder;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Runtime.InteropServices;
using ZXing.Common;
using ZXing;
using ZXing.QrCode.Internal;

namespace EQUOR.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataDBContext _context;

        public ProductsController(DataDBContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'DataDBContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        private double CalculateCarbonFootprint(int qWaterUsed, int qEnergy, int qWaste, string tipeTransport)
        {
            double carbonFootprint;

            // Convertir las cantidades a las unidades adecuadas según el tipo de transporte
            switch (tipeTransport)
            {
                case "Avión":
                    qWaterUsed = (int)(qWaterUsed * 0.7);
                    qEnergy = (int)(qEnergy * 2.4);
                    qWaste = (int)(qWaste * 1.3);
                    break;
                case "Barco":
                    qWaterUsed = (int)(qWaterUsed * 1.5);
                    qEnergy = (int)(qEnergy * 0.5);
                    qWaste = (int)(qWaste * 1.1);
                    break;
                case "Camión":
                    qWaterUsed = (int)(qWaterUsed * 1.1);
                    qEnergy = (int)(qEnergy * 1.2);
                    qWaste = (int)(qWaste * 1.2);
                    break;
                case "Tren":
                    qWaterUsed = (int)(qWaterUsed * 0.9);
                    qEnergy = (int)(qEnergy * 0.8);
                    qWaste = (int)(qWaste * 0.9);
                    break;
                default:
                    break;
            }

            // Huella de carbono en toneladas
            carbonFootprint = ((qWaterUsed * 0.002) + (qEnergy * 0.0009) + (qWaste * 0.001)) / 1000;

            return carbonFootprint;
        }

        private async Task<int> GetProductSearchCount(int productId)
        {
            //Metodo que cuenta los registros de la tabla de productsearches
            var productSearches = await _context.ProductSearches.Where(ps => ps.ProductId == productId).ToListAsync();
            return productSearches.Count();
        }

        private byte[] GenerateQRCode(String text)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 200,
                    Width = 200,
                    Margin = 10
                }
            };

            var pixelData = barcodeWriter.Write(text);
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                                                 ImageLockMode.WriteOnly,
                                                 bitmap.PixelFormat);

            Marshal.Copy(pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
            bitmap.UnlockBits(bmpData);

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                // Calcular la huella de carbono
                double carbonFootprint = CalculateCarbonFootprint(productViewModel.QWaterUsed,
                                                                  productViewModel.QEnergy,
                                                                  productViewModel.QWaste,
                                                                  productViewModel.TipeTransport
                                                                  );

                // Generar el código QR
                string qrCodeText = $"{productViewModel.IdProduct} {productViewModel.Name} {carbonFootprint}";
                byte[] qrCodeBytes = GenerateQRCode(qrCodeText);
                var product = new Product
                {
                    Name = productViewModel.Name,
                    DesProduct = productViewModel.DesProduct,
                    TipeTransport = productViewModel.TipeTransport,
                    QWaterUsed = productViewModel.QWaterUsed,
                    QEnergy = productViewModel.QEnergy,
                    QWaste = productViewModel.QWaste,
                    CodigoQR = qrCodeBytes,
                    CarbonFootprint = carbonFootprint,
                    TimeSearch = await GetProductSearchCount(productViewModel.IdProduct)
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Name,DesProduct,TipeTransport,QWaterUsed,QEnergy,QWaste,CodigoQR,CarbonFootprint,TimeSearch")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DataDBContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
    }
}
